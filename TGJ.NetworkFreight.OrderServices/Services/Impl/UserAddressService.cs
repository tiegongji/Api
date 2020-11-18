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
            throw new NotImplementedException();
        }

        public IEnumerable<dynamic> GetList(int userid)
        {
            throw new NotImplementedException();
        }
    }
}
