﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.OrderSercive;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.OrderService
{
    /// <summary>
    /// 订单微服务客户端
    /// </summary>
    [MicroClient("http", "OrderServices")]
    public interface IOrderClient
    {
        /// <summary>
        /// 获取订单
        /// </summary>
        [GetPath("/Orders")]

        public dynamic GetOrder();


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
        /// 获取订单状态成交额
        /// </summary>
        [GetPath("/Orders/StateTurnover/{userId}")]

        public StateTurnoverDto GetOrderStateTurnover(int userId);

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
        [PostPath("/Orders/Add")]

        public dynamic Add(OrderDetailPo entity);

        /// <summary>
        /// 获取订单列表
        /// </summary>
        [GetPath("/Orders/GetList")]

        public dynamic GetList(int userId, int pageIndex, int pageSize, int? status);

        /// <summary>
        /// 获取订单详情
        /// </summary>
        [PostPath("/Orders/GetDetail/{userId}/{OrderNo}")]

        public dynamic GetDetail(int userId, string OrderNo);


        /// <summary>
        /// 取消订单
        /// </summary>
        [PostPath("/Orders/Cancel")]

        public dynamic Cancel(OrderCancelDto entity);

        /// <summary>
        /// 指定司机
        /// </summary>
        [PostPath("/Orders/UpdateCarrierUser")]

        public dynamic UpdateCarrierUser(Order entity);

        /// <summary>
        ///确认回单
        /// </summary>
        [PostPath("/Orders/Confirm")]

        public dynamic Confirm(Order model);

        /// <summary>
        /// 更新价格
        /// </summary>
        [PostPath("/Orders/UpdateMoney")]

        public dynamic UpdateMoney(Order entity);
        /// <summary>
        /// 装货
        /// </summary>
        [PostPath("/Orders/UpdateLoading")]

        public dynamic UpdateLoading(OrderPo entity);
        /// <summary>
        /// 卸货
        /// </summary>
        [PostPath("/Orders/UpdateUnLoading")]

        public dynamic UpdateUnLoading(OrderPo model);
        /// <summary>
        /// 司机订单列表
        /// </summary>
        [GetPath("/Orders/GetWayBillList")]

        public dynamic GetWayBillList(int userId, int pageIndex, int pageSize, int? status);
        /// <summary>
        /// 上传图片
        /// </summary>
        [PostPath("/Orders/AddOrderReceiptImage")]

        public dynamic AddOrderReceiptImage(OrderReceiptImage model);
        /// <summary>
        /// 第三方订单
        /// </summary>
        [GetPath("/Orders/GetThirdList")]

        public dynamic GetThirdList(int userId, int pageIndex, int pageSize, int type,string orderNo);

        /// <summary>
        /// 关联第三方订单
        /// </summary>
        [PostPath("/Orders/AddAreaRelation")]

        public dynamic AddAreaRelation(AreaRelation entity);
    }
}
