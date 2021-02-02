using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using TGJ.NetworkFreight.Commons.AutoMappers;
using TGJ.NetworkFreight.Commons.Users;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.OrderSercive;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.OrderService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.UserService;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{    /// <summary>
     /// 订单聚合控制器
     /// </summary>
    [Route("api/Order")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderClient orderClient;
        private readonly IUserClient userClient;
        private readonly IMemoryCache memoryCache;
        public OrderController(IOrderClient orderClient, IUserClient userClient, IMemoryCache memoryCache)
        {
            this.orderClient = orderClient;
            this.userClient = userClient;
            this.memoryCache = memoryCache;
        }

        /// <summary>
        /// 获取订单列表
        /// </summary>
        /// <param name="sysUser"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetOrder()
        {
            return Ok(orderClient.GetOrder());
        }

        /// <summary>
        /// 司机搜索
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        [HttpGet("Drivers")]
        public ActionResult<IEnumerable<dynamic>> GetDrivers(string content)
        {
            var user = userClient.GetUserByKey(content);

            if (user == null)
                return Ok("未查到结果");

            var entity = AutoMapperHelper.AutoMapTo<User, DriversDto>(user);

            return Ok(entity);
        }

        /// <summary>
        /// 订单类型统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("Gather")]
        public ActionResult<GatherDto> GetOrderGather(SysUser sysUser)
        {
            return orderClient.GetOrderGather(sysUser.UserId);
        }
        /// <summary>
        /// 订单金额统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("Turnover")]
        public ActionResult<TurnoverDto> GetOrderTurnover(SysUser sysUser)
        {
            var order = orderClient.GetOrderTurnover(sysUser.UserId);

            var user = userClient.GetUserById(sysUser.UserId);

            if (user != null)
            {
                order.HasAuthenticated = user.HasAuthenticated;

                if (order.HasAuthenticated == true)
                {
                    order.Name = user.Name;
                    order.IdCard = user.IDCard;
                }
            }

            return order;
        }

        /// <summary>
        /// 司机端订单金额统计
        /// </summary>
        /// <returns></returns>
        [HttpGet("StateTurnover")]
        public ActionResult<StateTurnoverDto> GetOrderStateTurnover(SysUser sysUser)
        {
            var order = orderClient.GetOrderStateTurnover(sysUser.UserId);

            var user = userClient.GetUserById(sysUser.UserId);

            if (user != null)
            {
                order.HasAuthenticated = user.HasAuthenticated;

                if (order.HasAuthenticated == true)
                {
                    order.Name = user.Name;
                    order.IdCard = user.IDCard;
                }
            }

            return order;
        }

        /// <summary>
        /// 货物类型列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInitCategoryList")]
        public ActionResult<dynamic> GetInitCategoryList()
        {
            return orderClient.GetInitCategoryList();
        }

        /// <summary>
        /// 卡车类型列表
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetInitTruckList")]
        public ActionResult<dynamic> GetInitTruckList()
        {
            return orderClient.GetInitTruckList();
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult<dynamic> Add(SysUser sysUser, [FromForm] OrderDetailPo entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.Add(entity);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost("GetList")]
        public ActionResult<IEnumerable<dynamic>> GetList(SysUser sysUser, int pageIndex, int pageSize, int? status)
        {
            return orderClient.GetList(sysUser.UserId, pageIndex, pageSize, status);
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [HttpPost("GetDetail/{OrderNo}")]
        public ActionResult<dynamic> GetDetail(SysUser sysUser, string OrderNo)
        {
            return orderClient.GetDetail(sysUser.UserId, OrderNo);
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="OrderNo"></param>
        /// <param name="Reason"></param>
        /// <param name="Description"></param>
        /// <returns></returns>
        [HttpPost("Cancel")]
        public ActionResult<dynamic> Cancel(SysUser sysUser, string OrderNo, string Reason, string Description)
        {
            var entity = new OrderCancelDto();
            entity.OrderNo = OrderNo;
            entity.UserID = sysUser.UserId;
            entity.Description = Description;
            entity.Reason = Reason;
            return orderClient.Cancel(entity);
        }

        /// <summary>
        /// 指定司机
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateCarrierUser")]
        public ActionResult<dynamic> UpdateCarrierUser(SysUser sysUser, int CarrierUserID, string OrderNo)
        {
            var entity = new Order();
            entity.OrderNo = OrderNo;
            entity.UserID = sysUser.UserId;
            entity.CarrierUserID = CarrierUserID;
            return orderClient.UpdateCarrierUser(entity);
        }


        /// <summary>
        /// 绑定司机
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="UserId"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [HttpPost("BindCarrierUser")]
        public ActionResult<dynamic> BindCarrierUser(SysUser sysUser, int UserId, string OrderNo)
        {
            var entity = new Order();
            entity.OrderNo = OrderNo;
            entity.UserID = UserId;
            entity.CarrierUserID = sysUser.UserId;
            return orderClient.UpdateCarrierUser(entity);
        }


        /// <summary>
        /// 确认回单
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [HttpPost("Confirm")]
        public ActionResult<dynamic> Confirm(SysUser sysUser, string OrderNo)
        {
            var entity = new Order();
            entity.OrderNo = OrderNo;
            entity.UserID = sysUser.UserId;
            return orderClient.Confirm(entity);
        }
        /// <summary>
        /// 更新价格
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="OrderNo"></param>
        /// <param name="TotalAmount"></param>
        /// <param name="CarrierTruckID"></param>
        /// <returns></returns>
        [HttpPost("UpdateMoney")]
        public ActionResult<dynamic> UpdateMoney(SysUser sysUser, string OrderNo, decimal TotalAmount, int CarrierTruckID)
        {
            var entity = new Order();
            entity.OrderNo = OrderNo;
            entity.UserID = sysUser.UserId;
            entity.TotalAmount = TotalAmount;
            entity.CarrierTruckID = CarrierTruckID;
            return orderClient.UpdateMoney(entity);
        }
        /// <summary>
        /// 装货
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UpdateLoading")]
        public ActionResult<dynamic> UpdateLoading(SysUser sysUser, [FromForm] OrderImgFormDto model)
        {
            var entity = new OrderPo();
            entity.OrderNo = model.OrderNo;
            entity.UserID = sysUser.UserId;
            entity.imgs = JsonConvert.DeserializeObject<List<OrderReceiptImage>>(model.imgs);
            return orderClient.UpdateLoading(entity);
        }
        /// <summary>
        /// 卸货
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("UpdateUnLoading")]
        public ActionResult<dynamic> UpdateUnLoading(SysUser sysUser, [FromForm] OrderImgFormDto model)
        {
            var entity = new OrderPo();
            entity.OrderNo = model.OrderNo;
            entity.UserID = sysUser.UserId;
            entity.imgs = JsonConvert.DeserializeObject<List<OrderReceiptImage>>(model.imgs);
            return orderClient.UpdateUnLoading(entity);
        }

        /// <summary>
        /// 司机订单列表
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="status">null：运单列表,2：货源列表</param>
        [HttpPost("GetWayBillList")]
        public ActionResult<IEnumerable<dynamic>> GetWayBillList(SysUser sysUser, int pageIndex, int pageSize, int? status)
        {
            return orderClient.GetWayBillList(sysUser.UserId, pageIndex, pageSize, status);
        }

        /// <summary>
        /// 上传订单图片
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("AddOrderReceiptImage")]
        public ActionResult<dynamic> AddOrderReceiptImage(SysUser sysUser, [FromForm] OrderImgFormDto model)
        {
            var entity = new OrderReceiptImage();
            entity.OrderNo = model.OrderNo;
            entity.FileUrl = model.imgs;
            return orderClient.AddOrderReceiptImage(entity);
        }

        [HttpPost("AddOrderReceiptImageFile")]
        public ActionResult<dynamic> AddOrderReceiptImageFile()
        {
            var file = HttpContext.Request.Form.Files[0];
            System.IO.Stream fs = file.OpenReadStream();
            string strRet = null;
            try
            {
                if (fs == null) return null;
                byte[] bt = new byte[fs.Length];
                fs.Read(bt, 0, bt.Length);
                strRet = System.Convert.ToBase64String(bt);
                fs.Close();
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            var entity = new OrderReceiptImage();
            entity.OrderNo = "";
            entity.FileUrl = strRet;
            return orderClient.AddOrderReceiptImage(entity);
        }


        /// <summary>
        /// 第三方订单列表
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="type"></param>
        [HttpPost("GetThirdList")]
        public ActionResult<IEnumerable<dynamic>> GetThirdList(SysUser sysUser, int pageIndex, int pageSize, int type, string orderNo)
        {
            return orderClient.GetThirdList(sysUser.UserId, pageIndex, pageSize, type, orderNo);
        }

        /// <summary>
        /// 关联第三方订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("AddAreaRelation")]
        public ActionResult<dynamic> AddAreaRelation([FromForm] AreaRelation entity)
        {
            return orderClient.AddAreaRelation(entity);
        }
    }
}
