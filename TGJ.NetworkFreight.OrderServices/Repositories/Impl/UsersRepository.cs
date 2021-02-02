using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Context;
using TGJ.NetworkFreight.OrderServices.Models;
using TGJ.NetworkFreight.OrderServices.Repositories.Interface;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Repositories.Impl
{
    public class UsersRepository : IUsersRepository
    {
        public OrderContext context;
        public UsersRepository(OrderContext _context)
        {
            this.context = _context;
        }

        public User Get(int id)
        {
            return context.Users.Where(a => a.Id == id).FirstOrDefault();
        }
    }
}