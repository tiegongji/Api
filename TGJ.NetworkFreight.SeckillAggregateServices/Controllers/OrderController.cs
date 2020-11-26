using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using TGJ.NetworkFreight.Commons.Users;
using TGJ.NetworkFreight.OrderServices.Models;

using TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.OrderService;

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
        public OrderController(IOrderClient orderClient)
        {
            this.orderClient = orderClient;
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
            return orderClient.GetOrderTurnover(sysUser.UserId);
        }

        /// <summary>
        /// 货物类型列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetInitCategoryList")]
        public ActionResult<object> GetInitCategoryList()
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
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Add")]
        public ActionResult<dynamic> Add(SysUser sysUser, [FromQuery] OrderDetailDto entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.Add(entity);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("GetList")]
        public ActionResult<IEnumerable<dynamic>> GetList(SysUser sysUser, int? status)
        {
            return orderClient.GetList(sysUser.UserId, status);
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="userId"></param>
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
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Cancel")]
        public ActionResult<dynamic> Cancel(SysUser sysUser, [FromQuery] Order entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.Cancel(entity);
        }

        /// <summary>
        /// 指定司机
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("UpdateCarrierUser")]
        public ActionResult<dynamic> UpdateCarrierUser(SysUser sysUser, [FromQuery] Order entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.UpdateCarrierUser(entity);
        }

        /// <summary>
        /// 物流端上传回单
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("UpdateUpload")]
        public ActionResult<dynamic> UpdateUpload(SysUser sysUser, [FromQuery] OrderDto entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.UpdateUpload(entity);
        }
        /// <summary>
        /// 更新价格
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("UpdateMoney")]
        public ActionResult<dynamic> UpdateMoney(SysUser sysUser, [FromQuery] Order entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.UpdateMoney(entity);
        }
        /// <summary>
        /// 装货
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("UpdateLoading")]
        public ActionResult<dynamic> UpdateLoading(SysUser sysUser, [FromQuery] Order entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.UpdateLoading(entity);
        }
        /// <summary>
        /// 卸货
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("UpdateUnLoading")]
        public ActionResult<dynamic> UpdateUnLoading(SysUser sysUser, [FromQuery] OrderDto entity)
        {
            entity.UserID = sysUser.UserId;
            return orderClient.UpdateUnLoading(entity);
        }
    }
}
