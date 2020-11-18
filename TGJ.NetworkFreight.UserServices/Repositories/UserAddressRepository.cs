using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Context;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    /// <summary>
    /// 用户地址仓储实现
    /// </summary>
    public class UserAddressRepository : IUserAddressRepository
    {
        public UserContext UserContext;
        public UserAddressRepository(UserContext UserContext)
        {
            this.UserContext = UserContext;
        }
        public void Create(UserAddress UserAddress)
        {
            UserContext.UserAddresss.Add(UserAddress);
            UserContext.SaveChanges();
        }

        public void Delete(UserAddress UserAddress)
        {
            UserContext.UserAddresss.Remove(UserAddress);
            UserContext.SaveChanges();
        }

        public UserAddress GetUserAddressById(int userId, int id)
        {
            return UserContext.UserAddresss.FirstOrDefault(a => a.UserID == userId && a.Id == id);
        }

        public IEnumerable<UserAddress> GetUserAddresss(int userId)
        {
            return UserContext.UserAddresss.Where(a => a.UserID == userId).ToList();
        }

        public void Update(UserAddress UserAddress)
        {
            UserContext.UserAddresss.Update(UserAddress);
            UserContext.SaveChanges();
        }

        public bool UserAddressExists(int id)
        {
            return UserContext.UserAddresss.Any(e => e.Id == id);
        }
    }
}
