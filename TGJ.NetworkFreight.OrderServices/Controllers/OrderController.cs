using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.OrderServices.Dto;
using TGJ.NetworkFreight.OrderServices.Models;
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
        private readonly IUserAddressService IUserAddressService;
        private readonly IMapper mapper;

        public OrderController(IOrderService _IOrderService, IUserAddressService _IUserAddressService, IMapper _mapper)
        {
            IOrderService = _IOrderService;
            IUserAddressService = _IUserAddressService;
            mapper = _mapper;
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
        [HttpPost("Add")]
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
        public ActionResult<IEnumerable<dynamic>> GetList(int userId, int pageIndex, int pageSize, int? status)
        {
            return IOrderService.GetList(userId, pageIndex, pageSize, status).ToList();
        }

        /// <summary>
        /// 订单详情
        /// </summary>
        /// <param name="OrderNo"></param>
        /// <returns></returns>
        [HttpPost("GetDetail/{OrderNo}")]
        public ActionResult<dynamic> GetDetail(int userId, string OrderNo)
        {
            var result = IOrderService.GetDetail(userId, OrderNo);

            if (result == null)
            {
                return NotFound(result);
            }

            return result;
        }
        /// <summary>
        /// 订单统计
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Gather/{userId}")]
        public ActionResult<dynamic> GetOrderGather(int userId)
        {
            var result = IOrderService.GetOrderGather(userId);

            if (result == null)
            {
                return NotFound(result);
            }

            return result;
        }

        /// <summary>
        /// 金额统计
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("Turnover/{userId}")]
        public ActionResult<OrderTurnoverDto> GetOrderTurnover(int userId)
        {
            var result = IOrderService.GetOrderTurnover(userId);

            if (result == null)
            {
                return NotFound(result);
            }

            return result;
        }

        /// <summary>
        /// 新增地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("Address/Add")]
        public ActionResult AddAddress([FromQuery] UserAddress entity)
        {
            IUserAddressService.Add(entity);
            return Ok("添加成功");
        }
        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpDelete("Address/{id}")]
        public ActionResult DelAddress(int id, int userid)
        {
            IUserAddressService.Delete(id, userid);
            return Ok("删除成功");
        }

        /// <summary>
        /// 地址列表
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpPost("GetAddressList")]
        public ActionResult<IEnumerable<dynamic>> GetAddressList(int userId)
        {
            return IUserAddressService.GetList(userId).ToList();
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("Cancel")]
        public ActionResult Cancel(Order entity)
        {
            IOrderService.UpdateCancel(entity);
            return Ok("取消成功");
        }

        /// <summary>
        /// 指定司机
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateCarrierUser")]
        public ActionResult UpdateCarrierUser(Order entity)
        {
            IOrderService.UpdateCarrierUser(entity);
            return Ok("操作成功");
        }

        /// <summary>
        /// 物流端上传回单
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateUpload")]
        public ActionResult UpdateUpload(OrderDto model)
        {
            var entity = mapper.Map<Order>(model);
            IOrderService.UpdateUpload(entity, model.imgs);
            return Ok("上传成功");
        }

        /// <summary>
        /// 更新价格
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateMoney")]
        public ActionResult UpdateMoney(Order entity)
        {
            IOrderService.UpdateMoney(entity);
            return Ok("添加成功");
        }

        /// <summary>
        /// 装货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateLoading")]
        public ActionResult UpdateLoading(Order entity)
        {
            IOrderService.UpdateLoading(entity);
            return Ok("装货成功");
        }


        /// <summary>
        /// 卸货
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost("UpdateUnLoading")]
        public ActionResult UpdateUnLoading(OrderDto model)
        {
            var entity = mapper.Map<Order>(model);
            IOrderService.UpdateUpload(entity, model.imgs);
            return Ok("上传成功");
        }
    }
}
