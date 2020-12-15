using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
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
        public ActionResult GetOrder(SysUser sysUser)
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

            order.HasAuthenticated = user?.HasAuthenticated;

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

            order.HasAuthenticated = user?.HasAuthenticated;

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
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult<dynamic> Add(OrderDetailPo entity)
        {
            //entity.UserID = sysUser.UserId;
            return orderClient.Add(entity);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
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
        /// <param name="entity"></param>
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
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("BindCarrierUser")]
        public ActionResult<dynamic> BindCarrierUser(int UserId, int CarrierUserID, string OrderNo)
        {
            var entity = new Order();
            entity.OrderNo = OrderNo;
            entity.UserID = UserId;
            entity.CarrierUserID = CarrierUserID;
            return orderClient.UpdateCarrierUser(entity);
        }


        /// <summary>
        /// 确认回单
        /// </summary>
        /// <param name="entity"></param>
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
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateMoney")]
        public ActionResult<dynamic> UpdateMoney(SysUser sysUser, string OrderNo, decimal TotalAmount)
        {
            var entity = new Order();
            entity.OrderNo = OrderNo;
            entity.UserID = sysUser.UserId;
            entity.TotalAmount = TotalAmount;
            return orderClient.UpdateMoney(entity);
        }
        /// <summary>
        /// 装货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateLoading")]
        public ActionResult<dynamic> UpdateLoading(OrderPo entity)
        {
            //var entity = new Order();
            //entity.OrderNo = OrderNo;
            //entity.UserID = sysUser.UserId;
            return orderClient.UpdateLoading(entity);
        }
        /// <summary>
        /// 卸货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateUnLoading")]
        public ActionResult<dynamic> UpdateUnLoading(OrderPo entity)
        {
            //entity.UserID = UserID;
            return orderClient.UpdateUnLoading(entity);
        }

        /// <summary>
        /// 司机订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="status">null：运单列表,2：货源列表</param>
        [HttpPost("GetWayBillList")]
        public ActionResult<IEnumerable<dynamic>> GetWayBillList(SysUser sysUser, int pageIndex, int pageSize, int? status)
        {
            return orderClient.GetWayBillList(sysUser.UserId, pageIndex, pageSize, status);
        }
    }
}
