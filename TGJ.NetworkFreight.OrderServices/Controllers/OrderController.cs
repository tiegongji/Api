using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Route("Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        // GET: api/Orders
        [HttpGet]
        public ActionResult GetOrders()
        {
            return new JsonResult(
                from c in User.Claims select new { c.Type, c.Value });
        }
    }
}
