using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using TGJ.NetworkFreight.OrderServices.Services.Interface;
using Microsoft.Extensions.Configuration;
using TGJ.NetworkFreight.OrderServices.Extend;
using TGJ.NetworkFreight.Commons.Extend;
using static TGJ.NetworkFreight.OrderServices.Models.Enum.EnumHelper;

namespace TGJ.NetworkFreight.OrderServices.Services.Impl
{
    public class OrderService : IOrderService
    {
        public readonly IInitCategoryRepository IInitCategoryRepository;
        public readonly IInitTruckRepository IInitTruckRepository;
        public readonly IOrderRepository IOrderRepository;
        public IConfiguration IConfiguration { get; }
        public OrderService(IInitCategoryRepository IInitCategoryRepository, IInitTruckRepository IInitTruckRepository, IOrderRepository IOrderRepository, IConfiguration IConfiguration)
        {
            this.IInitCategoryRepository = IInitCategoryRepository;
            this.IInitTruckRepository = IInitTruckRepository;
            this.IOrderRepository = IOrderRepository;
            this.IConfiguration = IConfiguration;
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
            orderGather.Dispatch = orders.Count(a => a.TradeStatus == (int)EnumOrderStatus.Start);
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
        }

        public void Confirm(Order entity)
        {
            IOrderRepository.Confirm(entity);
        }

        public void UpdateMoney(Order entity)
        {
            IOrderRepository.UpdateMoney(entity);
        }

        public void UpdateLoading(OrderDto entity)
        {
            string accessKeyId = IConfiguration["Ali:accessKeyId"];
            string accessKeySecret = IConfiguration["Ali:accessKeySecret"];
            string EndPoint = IConfiguration["Ali:EndPoint"];
            string bucketName = IConfiguration["Ali:bucketName"];
            string url = IConfiguration["Ali:url"];
            var list = new List<OrderReceiptImage>();
            var now = DateTime.Now;
            var filepath = url + now.Year + "/" + now.Month + "/" + now.Day + "/";
            foreach (var item in entity.imgs)
            {
                var filename = "TMS/" + filepath + Guid.NewGuid().ToString() + ".jpg";
                var res = ALiOSSHelper.Upload(filename, item.FileUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);
                var model = new OrderReceiptImage();
                model.FileUrl = filename;
                list.Add(model);
            }
            entity.imgs = list;
            IOrderRepository.UpdateLoading(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public void UpdateUnLoading(OrderDto entity)
        {
            string accessKeyId = IConfiguration["Ali:accessKeyId"];
            string accessKeySecret = IConfiguration["Ali:accessKeySecret"];
            string EndPoint = IConfiguration["Ali:EndPoint"];
            string bucketName = IConfiguration["Ali:bucketName"];
            string url = IConfiguration["Ali:url"];
            var list = new List<OrderReceiptImage>();
            var now = DateTime.Now;
            var filepath = url + now.Year + "/" + now.Month + "/" + now.Day + "/";
            foreach (var item in entity.imgs)
            {
                var filename = "TMS/"+ filepath + Guid.NewGuid().ToString() + ".jpg";
                var res = ALiOSSHelper.Upload(filename, item.FileUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);
                var model = new OrderReceiptImage();
                model.FileUrl = filename;
                list.Add(model);
            }
            entity.imgs = list;
            IOrderRepository.UpdateUnLoading(entity);
        }

        public IEnumerable<dynamic> GetWayBillList(int userId, int pageIndex, int pageSize, int? status)
        {
            return IOrderRepository.GetWayBillList(userId, pageIndex, pageSize, status);
        }


        //public void UpdateCarrierUser(Order entity)
        //{
        //    var model = IOrderRepository.Get(entity.ID);
        //    if (model.UserID == entity.UserID)
        //        throw new Exception("订单不存在");
        //    model.CarrierUserID = entity.CarrierUserID;
        //    model.LastUpdateTime = DateTime.Now;
        //    IOrderRepository.Update(model);
        //}

        //public void UpdateMoney(Order entity)
        //{
        //    var model = IOrderRepository.Get(entity.ID);
        //    if (model.UserID == entity.UserID)
        //        throw new Exception("订单不存在");
        //    model.TotalAmount = entity.TotalAmount;
        //    model.LastUpdateTime = DateTime.Now;
        //    IOrderRepository.Update(model);
        //}
    }
}
