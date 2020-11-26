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
    [MicroClient("http", "OrderServices")]
    public interface IAddressClient
    {
        /// <summary>
        /// 新增地址
        /// </summary>
        [PostPath("/Orders/Address/Add")]

        public dynamic AddAddress(UserAddress entity);
        /// <summary>
        /// 删除地址
        /// </summary>
        [DeletePath("/Orders/Address/{id}")]

        public dynamic DelAddress(int id, int userId);

        /// <summary>
        /// 获取地址列表
        /// </summary>
        [GetPath("/Orders/GetAddressList")]

        public dynamic GetList(int userId);


        //[PostPath("/Address")]
        //public UserAddress PostUserAddress(UserAddress UserAddress);


        //[PutPath("/Address/{id}")]
        //public void PutUserAddress(int id, UserAddress UserAddress);


        //[DeletePath("/Address/{userId}/{id}")]
        //public UserAddress DeletUserAddress(int userId, int id);
    }
}
