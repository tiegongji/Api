using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.AddressService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.AddressService;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 地址控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressClient addressClient;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="addressClient"></param>
        public AddressController(IAddressClient addressClient)
        {
            this.addressClient = addressClient;
        }

        /// <summary>
        /// 添加地址
        /// </summary>
        /// <param name="addressPo"></param>
        /// <returns></returns>
        [HttpPost]
        public UserAddress Post([FromForm] UserAddressPo addressPo)
        {
            // 1、userForm 转换成领域模型
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserAddressPo, UserAddress>();
            });

            IMapper mapper = configuration.CreateMapper();

            // 2、转换
            UserAddress userAddress = mapper.Map<UserAddressPo, UserAddress>(addressPo);
            userAddress.CreateTime = new DateTime();

            // 3、用户进行注册
            userAddress = addressClient.PostUserAddress(userAddress);

            return userAddress;
        }

        /// <summary>
        /// 获取地址列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public IEnumerable<UserAddress> GetUserAddresss(int userId)
        {
            return addressClient.GetUserAddresss(userId);
        }

        /// <summary>
        /// 获取单个地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{id}")]
        public UserAddress GetUserAddress(int userId, int id)
        {
            return addressClient.GetUserAddressById(userId, id);
        }

        /// <summary>
        /// 修改地址
        /// </summary>
        /// <param name="id"></param>
        /// <param name="addressPo"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public IActionResult PutUserAddress(int id, [FromForm] UserAddressPo addressPo)
        {
            // 1、userForm 转换成领域模型
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserAddressPo, UserAddress>();
            });

            IMapper mapper = configuration.CreateMapper();

            // 2、转换
            UserAddress userAddress = mapper.Map<UserAddressPo, UserAddress>(addressPo);
            userAddress.LastUpdateTime = new DateTime();

            addressClient.PutUserAddress(id, userAddress);

            return NoContent();
        }

        /// <summary>
        /// 删除地址
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/{id}")]
        public UserAddress DeleteUserAddress(int userId, int id)
        {
            return addressClient.DeletUserAddress(userId, id);
        }
    }
}
