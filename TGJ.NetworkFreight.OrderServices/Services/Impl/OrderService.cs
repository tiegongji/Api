using System;
using System.Collections.Generic;
using System.Linq;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using TGJ.NetworkFreight.OrderServices.Services.Interface;
using Microsoft.Extensions.Configuration;
using TGJ.NetworkFreight.Commons.Extend;
using static TGJ.NetworkFreight.OrderServices.Models.Enum.EnumHelper;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Services.Impl
{
    public class OrderService : IOrderService
    {
        private readonly IInitCategoryRepository IInitCategoryRepository;
        private readonly IInitTruckRepository IInitTruckRepository;
        private readonly IOrderRepository IOrderRepository;
        private readonly IOrderReceiptImageRepository IOrderReceiptImageRepository;
        private readonly IUsersRepository IUsersRepository;
        private readonly IWxTokenRepository IWxTokenRepository;
        private readonly IAreaRelationRepository IAreaRelationRepository;
        public IConfiguration IConfiguration { get; }
        public OrderService(IInitCategoryRepository IInitCategoryRepository, IInitTruckRepository IInitTruckRepository, IOrderRepository IOrderRepository, IConfiguration IConfiguration, IOrderReceiptImageRepository IOrderReceiptImageRepository, IUsersRepository IUsersRepository, IWxTokenRepository IWxTokenRepository, IAreaRelationRepository IAreaRelationRepository)
        {
            this.IInitCategoryRepository = IInitCategoryRepository;
            this.IInitTruckRepository = IInitTruckRepository;
            this.IOrderRepository = IOrderRepository;
            this.IConfiguration = IConfiguration;
            this.IOrderReceiptImageRepository = IOrderReceiptImageRepository;
            this.IUsersRepository = IUsersRepository;
            this.IWxTokenRepository = IWxTokenRepository;
            this.IAreaRelationRepository = IAreaRelationRepository;
        }

        public IEnumerable<dynamic> GetInitCategoryList()
        {
            return IInitCategoryRepository.GetList();
        }

        public IEnumerable<dynamic> GetInitTruckList()
        {
            return IInitTruckRepository.GetList();
        }

        public void Add(OrderDetailDto entity)
        {
            IOrderRepository.Add(entity);
        }

        public IEnumerable<dynamic> GetList(int userid, int pageIndex, int pageSize, int? status)
        {
            return IOrderRepository.GetList(userid, pageIndex, pageSize, status);
        }

        public dynamic GetDetail(int userid, string OrderNo)
        {
            return IOrderRepository.GetDetail(userid, OrderNo);
        }

        public OrderGatherDto GetOrderGather(int userid)
        {
            var orders = IOrderRepository.GetListByUid(userid, 1);
            var orderGather = new OrderGatherDto();
            orderGather.Dispatch = orders.Count(a => a.TradeStatus == (int)EnumOrderStatus.Start && a.ActionStatus != (int)EnumActionStatus.Unloading);
            orderGather.Confirm = orders.Count(a => a.TradeStatus == (int)EnumOrderStatus.Start && a.ActionStatus == (int)EnumActionStatus.Unloading);
            orderGather.Complete = orders.Count(a => a.TradeStatus == (int)EnumOrderStatus.Finish);

            return orderGather;
        }

        public OrderTurnoverDto GetOrderTurnover(int userid)
        {
            var orders = IOrderRepository.GetListByUid(userid, 1);
            var orderTurnover = new OrderTurnoverDto();

            var now = DateTime.Now;
            var startTime = new DateTime(now.Year, now.Month, 1);
            var endTime = startTime.AddMonths(1).AddDays(-1);

            orderTurnover.MonthlyTurnover = orders.Where(a => a.CreateTime > startTime && a.CreateTime < endTime).Sum(a => a.TotalAmount);

            orderTurnover.TotalTurnover = orders.Sum(a => a.TotalAmount);

            return orderTurnover;
        }

        public OrderStateTurnoverDto GetOrderStateTurnover(int userid)
        {
            var orders = IOrderRepository.GetListByUid(userid, 2);

            var orderDto = new OrderStateTurnoverDto();
            orderDto.CompleteTurnover = orders.Where(a => a.TradeStatus == (int)EnumOrderStatus.Finish).Sum(a => a.TotalAmount);
            orderDto.DispatchTurnover = orders.Where(a => a.TradeStatus == (int)EnumOrderStatus.Start).Sum(a => a.TotalAmount);

            return orderDto;
        }

        public void UpdateCancel(OrderCancelDto entity)
        {
            IOrderRepository.UpdateCancel(entity);
        }

        public void UpdateCarrierUser(Order entity)
        {
            IOrderRepository.UpdateCarrierUser(entity);
            SendMsg(entity, "您有新的订单");
        }

        public void Confirm(Order entity)
        {
            IOrderRepository.Confirm(entity);
            SendMsg(entity, "订单已完成");
        }

        public void UpdateMoney(Order entity)
        {
            IOrderRepository.UpdateMoney(entity);
            SendMsg(entity, "订单已报价");
        }

        /// <summary>
        /// 装货
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateLoading(OrderDto entity)
        {
            IOrderRepository.UpdateLoading(entity);
            SendMsg(new Order() { OrderNo=entity.OrderNo,UserID=entity.UserID}, "订单装货完成");
        }

        /// <summary>
        /// 卸货
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUnLoading(OrderDto entity)
        {
            IOrderRepository.UpdateUnLoading(entity);
            SendMsg(new Order() { OrderNo = entity.OrderNo, UserID = entity.UserID }, "订单卸货完成");
        }

        public IEnumerable<dynamic> GetWayBillList(int userId, int pageIndex, int pageSize, int? status)
        {
            return IOrderRepository.GetWayBillList(userId, pageIndex, pageSize, status);
        }


        public void AddOrderReceiptImage(OrderReceiptImage entity)
        {
            string accessKeyId = IConfiguration["Ali:accessKeyId"];
            string accessKeySecret = IConfiguration["Ali:accessKeySecret"];
            string EndPoint = IConfiguration["Ali:EndPoint"];
            string bucketName = IConfiguration["Ali:bucketName"];
            string url = IConfiguration["Ali:url"];
            var now = DateTime.Now;
            var filename = "Order/" + now.Year + "/" + now.Month + "/" + now.Day + "/" + Guid.NewGuid().ToString() + ".jpg";
            var res = ALiOSSHelper.Upload(filename, entity.FileUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);

            entity.FileUrl = url + filename;
            entity.Type = -1;
            entity.CreateTime = DateTime.Now;
            IOrderReceiptImageRepository.Add(entity);
        }

        private void SendMsg(Order entity, string msg)
        {
            var model = IOrderRepository.Get(entity.OrderNo);
            if (model == null)
            {
                throw new Exception("订单号不存在");
            }
            User user = null;
            if (model.UserID == entity.UserID)
            {
                user = IUsersRepository.Get(entity.CarrierUserID > 0 ? entity.CarrierUserID : model.CarrierUserID);
            }
            else if (model.CarrierUserID == entity.UserID)
            {
                user = IUsersRepository.Get(model.UserID);
            }
            if (user == null)
                throw new Exception("用户角色错误");

            var tokenModel = IWxTokenRepository.Get(user.RoleName.Value);
            if (tokenModel == null)
                throw new Exception("微信配置错误");

            if (tokenModel.Token == "" || tokenModel.UpdateTime.AddHours(1) < DateTime.Now)
            {
                var stringToken = GetToken(tokenModel.AppId, tokenModel.Secret);
                JObject objRes = JsonConvert.DeserializeObject<JObject>(stringToken);
                var token = objRes["access_token"].ToString();
                if (token == "")
                    throw new Exception("无效Token");
                tokenModel.Token = token;
                tokenModel.UpdateTime = DateTime.Now;
                IWxTokenRepository.Update(tokenModel);
            }
            var data = new
            {
                touser = user.wx_OpenID,
                template_id = tokenModel.Template_id,
                page = "home",
                miniprogram_state = "formal",
                lang = "zh_CN",
                data = new
                {
                    character_string1 = new
                    {
                        value = model.OrderNo
                    },
                    thing9 = new
                    {
                        value = msg
                    }
                }
            };
            var res = HttpsService.Post(JsonConvert.SerializeObject(data), "https://api.weixin.qq.com/cgi-bin/message/subscribe/send?access_token=" + tokenModel.Token);
        }


        /// <summary>
        /// 获取OpenId和SessionKey的Json数据包
        /// </summary>
        /// <param name="code">客户端发来的code</param>
        /// <returns>Json数据包</returns>
        private string GetToken(string appid, string secret)
        {
            string temp = "https://api.weixin.qq.com/cgi-bin/token?" +
                "appid=" + appid
                + "&secret=" + secret
                + "&grant_type=client_credential";

            return HttpsService.Get(temp);
        }


        public IEnumerable<dynamic> GetThirdList(int userId, int pageIndex, int pageSize, int type, string OrderNo)
        {
            if (type == 0)
                return IOrderRepository.GetList_G7(pageIndex, pageSize, OrderNo);
            else
                return IOrderRepository.GetList_YMM(pageIndex, pageSize, OrderNo);
        }


        public void AddAreaRelation(AreaRelation entity)
        {
            IAreaRelationRepository.Add(entity);
        }
    }
}
