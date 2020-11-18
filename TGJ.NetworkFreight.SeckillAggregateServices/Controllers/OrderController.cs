using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.OrderServices.Models;
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
    }
}
