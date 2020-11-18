using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Services;
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
        private readonly IMapper mapper;
        public OrderController(IOrderService _IOrderService, IMapper _mapper)
        {
            IOrderService = _IOrderService;
            mapper = _mapper;
        }

        /// <summary>
        /// 货物类型
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetInitCategoryList")]
        public ActionResult GetInitCategoryList()
        {
            return Ok(IOrderService.GetInitCategoryList());
        }

        /// <summary>
        /// 卡车类型
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetInitTruckList")]
        public ActionResult GetInitTruckList()
        {
            return Ok(IOrderService.GetInitTruckList());
        }

        [HttpPost("add")]
        public ActionResult Add(OrderDetailDto entity)
        {
            var OrderDetail = mapper.Map<OrderDetail>(entity);
            IOrderService.Add(OrderDetail);
            return Ok("添加成功");
        }


        [HttpPost("GetList")]
        public ActionResult GetList(int? status)
        {
            return Ok(IOrderService.GetList(0, status));

        }

        [HttpPost("GetDetail/{OrderNo}")]
        public ActionResult GetDetail(string OrderNo)
        {
            return Ok(IOrderService.GetDetail(0, OrderNo));
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
