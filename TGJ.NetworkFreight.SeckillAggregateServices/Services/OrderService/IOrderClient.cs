using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive;

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


        /// <summary>
        /// 获取订单成交量
        /// </summary>
        [GetPath("/Orders/Gather/{userId}")]

        public GatherDto GetOrderGather(int userId);


        /// <summary>
        /// 获取订单成交额
        /// </summary>
        [GetPath("/Orders/Turnover/{userId}")]

        public TurnoverDto GetOrderTurnover(int userId);


        /// <summary>
        /// 获取货物类型
        /// </summary>
        [GetPath("/Orders/GetInitCategoryList")]

        public dynamic GetInitCategoryList();

        /// <summary>
        /// 获取卡车类型
        /// </summary>
        [GetPath("/Orders/GetInitTruckList")]

        public dynamic GetInitTruckList();

        /// <summary>
        /// 新增订单
        /// </summary>
        [GetPath("/Orders/Add")]

        public dynamic Add(OrderDetailDto entity);

        /// <summary>
        /// 获取订单列表
        /// </summary>
        [GetPath("/Orders/GetList")]

        public dynamic GetList(int userId, int? status);

        /// <summary>
        /// 获取订单详情
        /// </summary>
        [GetPath("/Orders/GetDetail")]

        public dynamic GetDetail(int userId, string OrderNo);
    }
}
