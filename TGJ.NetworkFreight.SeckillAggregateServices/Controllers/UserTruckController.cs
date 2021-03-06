﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.Commons.Extend;
using TGJ.NetworkFreight.Commons.MemoryCaches;
using TGJ.NetworkFreight.Commons.Users;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserTruckService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.UserTruckService;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 用户车辆控制器
    /// </summary>
    [Route("api/UserTruck")]
    [ApiController]
    [Authorize]
    public class UserTruckController : ControllerBase
    {
        private readonly IUserTruckClient userTruckClient;
        private readonly ICertificationClient certificationClient;
        private readonly ICaching caching;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userTruckClient"></param>
        /// <param name="certificationClient"></param>
        /// <param name="Configuration"></param>
        public UserTruckController(IUserTruckClient userTruckClient, ICertificationClient certificationClient, IConfiguration Configuration, ICaching caching)
        {
            this.userTruckClient = userTruckClient;
            this.certificationClient = certificationClient;
            this.Configuration = Configuration;
            this.caching = caching;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<dynamic> AddUserTruck(SysUser sysUser, [FromForm] UserTruckPo entity)
        {
            var flag = userTruckClient.Exists(sysUser.UserId, entity.VehicleNumber);

            if (flag)
                throw new BizException("车辆已存在");

            string accessKeyId = Configuration["Ali:accessKeyId"];
            string accessKeySecret = Configuration["Ali:accessKeySecret"];
            string EndPoint = Configuration["Ali:EndPoint"];
            string bucketName = Configuration["Ali:bucketName"];

            try
            {
                if (string.IsNullOrWhiteSpace(entity.TravelCardUrl))
                    throw new BizException("行驶证图片不能为空");

                var TravelCardUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                ALiOSSHelper.Upload(TravelCardUrl, entity.TravelCardUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);

                entity.TravelCardUrl = TravelCardUrl;

                if (!string.IsNullOrWhiteSpace(entity.TransportLicenseUrl))
                {
                    var TransportLicenseUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                    ALiOSSHelper.Upload(TransportLicenseUrl, entity.TransportLicenseUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);
                    entity.TransportLicenseUrl = TransportLicenseUrl;
                }

                if (!string.IsNullOrWhiteSpace(entity.BusinessLicenseUrl))
                {
                    var BusinessLicenseUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                    ALiOSSHelper.Upload(BusinessLicenseUrl, entity.BusinessLicenseUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);
                    entity.BusinessLicenseUrl = BusinessLicenseUrl;
                }

            }
            catch (Exception e)
            {
                throw new BizException("图片上传失败" + "/" + e.Message);
            }


            entity.IsValid = true;
            entity.UserID = sysUser.UserId;
            entity.CreateTime = DateTime.Now;

            var truck = userTruckClient.Add(entity);

            if (truck == null)
                return NotFound("添加失败");

            Task.Run(() =>
            {
                var trucks = userTruckClient.GetList(sysUser.UserId);
                caching.Update($"UserTruck{sysUser.UserId}", trucks);
            });

            return Ok(truck);
        }


        /// <summary>
        /// 更新
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        public ActionResult<dynamic> UpdateBusinessLicense(SysUser sysUser, [FromForm] BusinessLicensePo entity)
        {
            var userTruck = userTruckClient.GetUserTruckById(sysUser.UserId, entity.Id);

            if (userTruck == null)
                throw new BizException("车辆信息不存在");

            string accessKeyId = Configuration["Ali:accessKeyId"];
            string accessKeySecret = Configuration["Ali:accessKeySecret"];
            string EndPoint = Configuration["Ali:EndPoint"];
            string bucketName = Configuration["Ali:bucketName"];

            try
            {
                if (!string.IsNullOrWhiteSpace(entity.TransportLicenseUrl))
                {
                    var TransportLicenseUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                    ALiOSSHelper.Upload(TransportLicenseUrl, entity.TransportLicenseUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);

                    userTruck.TransportLicenseUrl = TransportLicenseUrl;
                }

                if (!string.IsNullOrWhiteSpace(entity.BusinessLicenseUrl))
                {
                    var BusinessLicenseUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                    ALiOSSHelper.Upload(BusinessLicenseUrl, entity.BusinessLicenseUrl, accessKeyId, accessKeySecret, EndPoint, bucketName);

                    userTruck.BusinessLicenseUrl = BusinessLicenseUrl;
                }

            }
            catch (Exception e)
            {
                throw new BizException("图片上传失败" + "/" + e.Message);
            }

            userTruck.RoadTransportCertificateNumber = entity.RoadTransportCertificateNumber;
            userTruck.RoadTransportManageNumber = entity.RoadTransportManageNumber;

            var truck = userTruckClient.Update(userTruck);

            if (truck == null)
                return NotFound("更新失败");

            Task.Run(() =>
            {
                var trucks = userTruckClient.GetList(sysUser.UserId);
                caching.Update($"UserTruck{sysUser.UserId}", trucks);
            });


            return Ok(truck);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sysUser"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public ActionResult<dynamic> Delete(SysUser sysUser, int id)
        {
            var truck = userTruckClient.Delete(sysUser.UserId, id);

            if (truck == null)
                return NotFound("删除对象不存在");

            return Ok(truck);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<dynamic>> GetList(SysUser sysUser)
        {
            var cacheKey = $"UserTruck{sysUser.UserId}";
            var truck = caching.Get(cacheKey);

            if (truck == null)
            {
                truck = userTruckClient.GetList(sysUser.UserId);

                if (truck == null)
                    return NotFound("未查到结果");

                Task.Run(() =>
                {
                    caching.Set(cacheKey, truck);
                });
            }

            return Ok(truck);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("MyUserList")]
        public ActionResult<IEnumerable<dynamic>> GetListByUserId(SysUser sysUser)
        {
            var cacheKey = $"UserTruckValid{sysUser.UserId}";
            var truck = caching.Get(cacheKey);

           // if (truck == null)
            {
                truck = userTruckClient.UserList(sysUser.UserId);

                if (truck == null)
                    return NotFound("未查到结果");

                Task.Run(() =>
                {
                    caching.Set(cacheKey, truck);
                });
            }

            return Ok(truck);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("UserList/{UserId}")]
        public ActionResult<IEnumerable<dynamic>> GetListByUserId(int UserId)
        {
            var cacheKey = $"UserTruckValid{UserId}";
            var truck = caching.Get(cacheKey);

            //if (truck == null)
            {
                truck = userTruckClient.UserList(UserId);

                if (truck == null)
                    return NotFound("未查到结果");

                Task.Run(() =>
                {
                    caching.Set(cacheKey, truck);
                });
            }

            return Ok(truck);
        }

    }
}
