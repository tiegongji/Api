using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.AddressService
{
    /// <summary>
    /// 用户地址微服务客户端
    /// </summary>
    [MicroClient("https", "UserServices")]
    public interface IAddressClient
    {
        [GetPath("/Address/{userId}")]
        public IEnumerable<UserAddress> GetUserAddresss(int userId);


        [GetPath("/Address/{userId}/{id}")]
        public UserAddress GetUserAddressById(int userId, int id);


        [PostPath("/Address")]
        public UserAddress PostUserAddress(UserAddress UserAddress);


        [PutPath("/Address/{id}")]
        public void PutUserAddress(int id, UserAddress UserAddress);


        [DeletePath("/Address/{userId}/{id}")]
        public UserAddress DeletUserAddress(int userId, int id);
    }
}
