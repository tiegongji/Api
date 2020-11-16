using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Repositories;

namespace TGJ.NetworkFreight.UserServices.Services
{
    /// <summary>
    /// 用户地址服务实现
    /// </summary>
    public class UserAddressService : IUserAddressService
    {
        public readonly IUserAddressRepository UserAddressRepository;

        public UserAddressService(IUserAddressRepository UserAddressRepository)
        {
            this.UserAddressRepository = UserAddressRepository;
        }

        public void Create(UserAddress UserAddress)
        {
            UserAddressRepository.Create(UserAddress);
        }

        public void Delete(UserAddress UserAddress)
        {
            UserAddressRepository.Delete(UserAddress);
        }

        public UserAddress GetUserAddressById(int userId, int id)
        {
            return UserAddressRepository.GetUserAddressById(userId, id);
        }

        public IEnumerable<UserAddress> GetUserAddresss(int userId)
        {
            return UserAddressRepository.GetUserAddresss(userId);
        }

        public void Update(UserAddress UserAddress)
        {
            UserAddressRepository.Update(UserAddress);
        }

        public bool UserAddressExists(int id)
        {
            return UserAddressRepository.UserAddressExists(id);
        }
    }
}
