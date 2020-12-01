using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Context;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    /// <summary>
    /// 用户车辆仓储实现
    /// </summary>
    public class UserTruckRepository : IUserTruckRepository
    {
        public UserContext UserContext;
        public UserTruckRepository(UserContext UserContext)
        {
            this.UserContext = UserContext;
        }

        public void Create(UserTruck UserTruck)
        {
            UserContext.UserTruck.Add(UserTruck);
            UserContext.SaveChanges();
        }

        public void Delete(UserTruck UserTruck)
        {
            UserContext.UserTruck.Remove(UserTruck);
            UserContext.SaveChanges();
        }

        public UserTruck GetUserTruckById(int userId, int id)
        {
            return UserContext.UserTruck.FirstOrDefault(a => a.Id == id && a.UserID == userId && a.IsValid == true);
        }

        public IEnumerable<UserTruck> GetUserTrucks(int userId)
        {
            return UserContext.UserTruck.Where(a => a.UserID == userId && a.IsValid == false).ToList();
        }
    }
}
