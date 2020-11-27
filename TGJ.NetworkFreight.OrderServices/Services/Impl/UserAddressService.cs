using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using TGJ.NetworkFreight.OrderServices.Services.Interface;

namespace TGJ.NetworkFreight.OrderServices.Services.Impl
{
    public class UserAddressService : IUserAddressService
    {
        public readonly IUserAddressRepository IUserAddressRepository;

        public UserAddressService(IUserAddressRepository IUserAddressRepository)
        {
            this.IUserAddressRepository = IUserAddressRepository;
        }
        public void Add(UserAddress entity)
        {
            IUserAddressRepository.Add(entity);
        }

        public void Delete(int id, int userid)
        {
            IUserAddressRepository.Delete(id,userid);
        }

        public IEnumerable<dynamic> GetList(int userid)
        {
            return IUserAddressRepository.GetList(userid);
        }
        public bool UserAddressExists(int id)
        {
            return IUserAddressRepository.UserAddressExists(id);
        }
    }
}
