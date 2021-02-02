using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserTruckService;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.UserTruckService
{
    /// <summary>
    /// 用户车辆微服务客户端
    /// </summary>
    [MicroClient("http", "UserServices")]
    public interface IUserTruckClient
    {
        /// <summary>
        /// 新增地址
        /// </summary>
        [PostPath("/UserTrucks")]
        public dynamic Add(UserTruckPo entity);

        /// <summary>
        /// 删除
        /// </summary>
        [DeletePath("/UserTrucks/{userId}/{id}")]
        public dynamic Delete(int userId, int id);

        /// <summary>
        /// 更新
        /// </summary>
        [PutPath("/UserTrucks")]
        public dynamic Update(UserTruck userTruckPo);

        /// <summary>
        /// 获取列表
        /// </summary>
        [GetPath("/UserTrucks/{userId}")]

        public dynamic GetList(int userId);

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        [GetPath("/UserTrucks/Exists/{userId}/{vehicleNumber}")]
        public bool Exists(int userId, string vehicleNumber);

        /// <summary>
        /// 获取单个
        /// </summary>
        [GetPath("/UserTrucks/{userId}/{id}")]

        public UserTruck GetUserTruckById(int userId, int id);

        /// <summary>
        /// 获取可用列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [GetPath("/UserTrucks/UserList/{userId}")]
        public dynamic UserList(int userId);
    }
}
