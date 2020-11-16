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
    [Route("api/[Address]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
    //    private readonly IAddressClient addressClient;
    //    public AddressController(IAddressClient addressClient)
    //    {
    //        this.addressClient = addressClient;
    //    }

    //    [HttpPost]
    //    public UserAddress Post([FromForm] UserAddressPo addressPo)
    //    {
    //        // 1、userForm 转换成领域模型
    //        var configuration = new MapperConfiguration(cfg =>
    //        {
    //            cfg.CreateMap<UserAddressPo, UserAddress>();
    //        });

    //        IMapper mapper = configuration.CreateMapper();

    //        // 2、转换
    //        UserAddress userAddress = mapper.Map<UserAddressPo, UserAddress>(addressPo);
    //        userAddress.CreateTime = new DateTime();

    //        // 3、用户进行注册
    //        userAddress = addressClient.PostUserAddress(userAddress);

    //        return userAddress;
    //    }

    //    [HttpGet("{userId}")]
    //    public IEnumerable<UserAddress> GetUserAddresss(int userId)
    //    {
    //        return addressClient.GetUserAddresss(userId);
    //    }

    //    [HttpGet("{userId}/{id}")]
    //    public UserAddress GetUserAddress(int userId, int id)
    //    {
    //        return addressClient.GetUserAddressById(userId, id);
    //    }

    //    [HttpPut("{id}")]
    //    public IActionResult PutUserAddress(int id, [FromForm] UserAddressPo addressPo)
    //    {
    //        // 1、userForm 转换成领域模型
    //        var configuration = new MapperConfiguration(cfg =>
    //        {
    //            cfg.CreateMap<UserAddressPo, UserAddress>();
    //        });

    //        IMapper mapper = configuration.CreateMapper();

    //        // 2、转换
    //        UserAddress userAddress = mapper.Map<UserAddressPo, UserAddress>(addressPo);
    //        userAddress.LastUpdateTime = new DateTime();

    //        addressClient.PutUserAddress(id, userAddress);

    //        return NoContent();
    //    }

    //    [HttpDelete("{userId}/{id}")]
    //    public UserAddress DeleteUserAddress(int userId, int id)
    //    {
    //        return addressClient.DeletUserAddress(userId, id);
    //    }
    }
}
