using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Services
{
    /// <summary>
    /// 用户地址服务接口
    /// </summary>
    public interface IUserAddressService
    {
        IEnumerable<UserAddress> GetUserAddresss(int userId);
        UserAddress GetUserAddressById(int userId, int id);
        void Create(UserAddress UserAddress);
        void Update(UserAddress UserAddress);
        void Delete(UserAddress UserAddress);
        bool UserAddressExists(int id);
    }
}
