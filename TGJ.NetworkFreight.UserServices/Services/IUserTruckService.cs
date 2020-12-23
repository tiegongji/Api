using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.UserServices.Services
{
    /// <summary>
    /// 用户车辆服务接口
    /// </summary>
    public interface IUserTruckService
    {
        IEnumerable<UserTruck> GetUserTrucks(int userId);
        UserTruck GetUserTruckById(int userId, int id);
        void Create(UserTruck UserTruck);
        void Update(UserTruck UserTruck);
        bool Exists(int userId, string vehicleNumber);
    }
}
