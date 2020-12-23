using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.AutoMappers;
using TGJ.NetworkFreight.Commons.Utils;
using TGJ.NetworkFreight.UserServices.Dtos.UserTruckService;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Services;

namespace TGJ.NetworkFreight.UserServices.Controllers
{
    /// <summary>
    /// 用户车辆控制器
    /// </summary>
    [Route("UserTrucks")]
    [ApiController]
    public class UserTruckController : ControllerBase
    {
        private readonly IUserTruckService userTruckService;

        public UserTruckController(IUserTruckService userTruckService)
        {
            this.userTruckService = userTruckService;
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="UserTruck"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<UserTruck> PostUserTruck(UserTruck UserTruck)
        {
            userTruckService.Create(UserTruck);
            return CreatedAtAction("GeUserTruck", new { id = UserTruck.Id }, UserTruck);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("{userId}")]
        public ActionResult<IEnumerable<UserTruckDto>> GetUserTrucks(int userId)
        {
            var models = userTruckService.GetUserTrucks(userId).ToList();

            if (models == null)
                return Ok("未查到结果");

            models.ForEach(x =>
            {
                x.Color = Util.GetColorValue(x.Color);
            });

            var entity = AutoMapperHelper.AutoMapTo<UserTruck, UserTruckDto>(models).ToList();

            return entity;
        }

        /// <summary>
        /// 获取单个
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{userId}/{id}")]
        public ActionResult<UserTruck> GetUserTruckById(int userId, int id)
        {
            return userTruckService.GetUserTruckById(userId, id);
        }

        /// <summary>
        /// 判断是否存在
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="vehicleNumber"></param>
        /// <returns></returns>
        [HttpGet("Exists/{userId}/{vehicleNumber}")]
        public ActionResult<bool> Exists(int userId, string vehicleNumber)
        {
            return userTruckService.Exists(userId, vehicleNumber);
        }

        /// <summary>
        /// 逻辑删除
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{userId}/{id}")]
        public ActionResult<UserTruck> DeleteUserTruck(int userId, int id)
        {
            var UserTruck = userTruckService.GetUserTruckById(userId, id);

            if (UserTruck == null)
            {
                return NotFound(UserTruck);
            }

            UserTruck.Status = 0;
            userTruckService.Update(UserTruck);

            return UserTruck;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="UserTruck"></param>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<UserTruck> UpdateUserTruck(UserTruck UserTruck)
        {
            if (UserTruck == null)
            {
                return NotFound(UserTruck);
            }

            UserTruck.Status = 0;
            userTruckService.Update(UserTruck);

            return UserTruck;
        }
    }
}
