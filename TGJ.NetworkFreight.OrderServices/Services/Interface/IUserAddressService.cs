using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Services.Interface
{
    public interface IUserAddressService
    {
        void Add(UserAddress entity);
        IEnumerable<dynamic> GetList(int userid);
    }
}
