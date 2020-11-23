﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Services.Interface;

namespace TGJ.NetworkFreight.OrderServices.Controllers
{
    /// <summary>
    /// 订单控制器
    /// </summary>
    [Route("Orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService IOrderService;

        public OrderController(IOrderService _IOrderService)
        {
            IOrderService = _IOrderService;
        }

        /// <summary>
        /// 货物类型
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetInitCategoryList")]
        public ActionResult<IEnumerable<dynamic>> GetInitCategoryList()
        {
            return IOrderService.GetInitCategoryList().ToList();
        }

        /// <summary>
        /// 卡车类型
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetInitTruckList")]
        public ActionResult<IEnumerable<dynamic>> GetInitTruckList()
        {
            return IOrderService.GetInitTruckList().ToList();
        }


        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public ActionResult Add(OrderDetailDto entity)
        {
            IOrderService.Add(entity);
            return Ok("添加成功");
        }


        /// <summary>
        /// 订单列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost("GetList")]
        public ActionResult<IEnumerable<dynamic>> GetList(int userId, int? status)
        {
            return IOrderService.GetList(userId, status).ToList();
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [HttpPost("GetDetail/{OrderNo}")]
        public ActionResult<dynamic> GetDetail(int userId, string OrderNo)
        {
            return IOrderService.GetDetail(userId, OrderNo);
        }

        [HttpGet("Gather/userId")]
        public ActionResult<OrderGatherDto> GetOrderGather(int userId)
        {
            return IOrderService.GetOrderGather(userId);
        }

        [HttpGet("Turnover/userId")]
        public ActionResult<OrderTurnoverDto> GetOrderTurnover(int userId)
        {
            return IOrderService.GetOrderTurnover(userId);
        }
    }
}
