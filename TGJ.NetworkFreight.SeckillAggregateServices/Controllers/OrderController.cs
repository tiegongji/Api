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

        [HttpGet]
        public ActionResult GetOrder(SysUser sysUser)
        {
            return Ok(sysUser);
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
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Add/{userId}")]
        public ActionResult<dynamic> Add(int userId, OrderDetailDto entity)
        {
            entity.UserID = userId;
            return orderClient.Add(entity);
        }

        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetList/{userId}")]
        public ActionResult<IEnumerable<dynamic>> GetList(int userId, int? status)
        {
            return orderClient.GetList(userId, status);
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [HttpGet("GetDetail/{userId}/{OrderNo}")]
        public ActionResult<dynamic> GetDetail(int userId, string OrderNo)
        {
            return orderClient.GetDetail(userId, OrderNo);
        }
    }
}
