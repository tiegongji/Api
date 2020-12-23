using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Repositories;

namespace TGJ.NetworkFreight.UserServices.Services
{
    /// <summary>
    /// 用户车辆服务实现
    /// </summary>
    public class UserTruckService : IUserTruckService
    {
        public readonly IUserTruckRepository UserTruckRepository;

        public UserTruckService(IUserTruckRepository UserTruckRepository)
        {
            this.UserTruckRepository = UserTruckRepository;
        }

        public void Create(UserTruck UserTruck)
        {
            UserTruckRepository.Create(UserTruck);
        }

        public void Update(UserTruck UserTruck)
        {
            UserTruckRepository.Update(UserTruck);
        }

        public UserTruck GetUserTruckById(int userId, int id)
        {
            return UserTruckRepository.GetUserTruckById(userId, id);
        }

        public IEnumerable<UserTruck> GetUserTrucks(int userId)
        {
            return UserTruckRepository.GetUserTrucks(userId);
        }

        public bool Exists(int userId, string vehicleNumber)
        {
            return UserTruckRepository.Exists(userId, vehicleNumber);
        }
    }
}
