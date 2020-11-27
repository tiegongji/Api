using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Interface
{
    public interface IUserAddressRepository
    {
        void Add(UserAddress entity);
        void Delete(int id, int userid);
        IEnumerable<dynamic> GetList(int userid);
        bool UserAddressExists(int id);
    }
}
