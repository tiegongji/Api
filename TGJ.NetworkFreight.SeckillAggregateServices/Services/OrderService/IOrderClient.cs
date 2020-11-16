using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.OrderService
{
    /// <summary>
    /// 订单微服务客户端
    /// </summary>
    [MicroClient("https", "OrderServices")]
    public interface IOrderClient
    {
        /// <summary>
        /// 获取订单
        /// </summary>
        [GetPath("/Orders")]

        public Order GetOrder();
    }
}
