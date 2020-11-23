using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.OrderService;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{    /// <summary>
     /// 订单聚合控制器
     /// </summary>
    [Route("api/Order")]
    [ApiController]
    //[Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderClient orderClient;
        public OrderController(IOrderClient orderClient)
        {
            this.orderClient = orderClient;
        }

        [HttpGet]
        public ActionResult GetOrder()
        {
            return new JsonResult(
                from c in User.Claims select new { c.Type, c.Value });
            //return orderClient.GetOrder();
        }

        [HttpGet("Gather/userId")]
        public ActionResult<GatherDto> GetOrderGather(int userId)
        {
            return orderClient.GetOrderGather(userId);
        }

        [HttpGet("Turnover/userId")]
        public ActionResult<TurnoverDto> GetOrderTurnover(int userId)
        {
            return orderClient.GetOrderTurnover(userId);
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
