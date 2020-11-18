using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class UserAddressRepository : IUserAddressRepository
    {
        public OrderContext context;
        public UserAddressRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public void Add(UserAddress entity)
        {
            context.UserAddress.Add(entity);
            context.SaveChanges();
        }

        public IEnumerable<dynamic> GetList(int userid)
        {
            return context.UserAddress.Where(a => a.UserID == userid && a.IsValid == true);
          
        }
    }
}
