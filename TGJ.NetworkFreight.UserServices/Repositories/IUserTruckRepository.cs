using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Repositories
{
    /// <summary>
    /// 用户车辆仓储接口
    /// </summary>
    public interface IUserTruckRepository
    {
        IEnumerable<UserTruck> GetUserTrucks(int userId);
        UserTruck GetUserTruckById(int userId, int id);
        void Create(UserTruck UserTruck);
        void Update(UserTruck UserTruck);
        bool Exists(int userId, string vehicleNumber);
        IEnumerable<UserTruck> GetUserTrucksByUser(Expression<Func<UserTruck, bool>> filter);
    }
}
