using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.Commons.Extend;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.UserService;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 认证控制器
    /// </summary>
    [Route("api/Certification")]
    [ApiController]
    [Authorize]
    public class CertificationController : ControllerBase
    {
        private readonly ICertificationClient certificationClient;
        private readonly IUserClient userClient;
        public IConfiguration Configuration { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificationClient"></param>
        public CertificationController(ICertificationClient certificationClient, IConfiguration Configuration, IUserClient userClient)
        {
            this.certificationClient = certificationClient;
            this.userClient = userClient;
            this.Configuration = Configuration;
        }

        /// <summary>
        /// 身份证实名接口
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("IdCard/Certification")]
        public ActionResult<decimal> RealNameCertification(string idCard, string name)
        {
            var dto = certificationClient.RealNameCertification(idCard, name);

            return Ok(dto);
        }

        /// <summary>
        /// 身份证OCR
        /// </summary>
        /// <param name="image">不包含图片头的，如data:image/jpg;base64,) </param>
        /// <param name="side">front：身份证带人脸一面，back：身份证带国徽片一面</param>
        /// <returns></returns>
        [HttpPost("IdCard/OCR")]
        public ActionResult<decimal> OCRIdCard(string image, string side)
        {
            var dto = certificationClient.OCRIdCard(image, side);

            return Ok(dto);
        }

        /// <summary>
        /// 银行卡OCR
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [HttpPost("Bank/OCR")]
        public ActionResult<decimal> OCRBank(string image)
        {
            var dto = certificationClient.OCRBank(image);

            return Ok(dto);
        }

        /// <summary>
        /// 银行卡认证
        /// </summary>
        /// <param name="backCard"></param>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("Bank/Certification")]
        public ActionResult<decimal> BankCertification(string backCard, string idCard, string name)
        {
            var dto = certificationClient.BankCertification(backCard, idCard, name);

            return Ok(dto);
        }

        /// <summary>
        /// 驾驶证识别
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type">1:正面/2:反面</param>
        /// <returns></returns>
        [HttpPost("Driver/OCR")]
        public ActionResult<decimal> OCRDriver(string image, string type)
        {
            var dto = certificationClient.OCRDriver(image, type);

            return Ok(dto);
        }

        /// <summary>
        /// 行驶证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type">1:正面/2:反面</param>
        /// <returns></returns>
        [HttpPost("Vehicle/OCR")]
        public ActionResult<decimal> OCRVehicle(string image, string type)
        {
            var dto = certificationClient.OCRVehicle(image, type);

            return Ok(dto);
        }

        /// <summary>
        /// 道路经营许可证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type">1:正面/2:反面</param>
        /// <returns></returns>
        [HttpPost("Permit/OCR")]
        public ActionResult<decimal> OCRPermit(string image, string type)
        {
            var dto = certificationClient.OCRPermit(image, type);

            return Ok(dto);
        }

        /// <summary>
        /// 司机实名认证
        /// </summary>
        /// <param name="certificationDto"></param>
        /// <returns></returns>
        [HttpPost("UserDriver")]
        public ActionResult<decimal> UserDriverCertification(DriverCertificationDto certificationDto)
        {
            var res = certificationClient.RealNameCertification(certificationDto.IDCard, certificationDto.Name);

            if (res == null || res.status != "01")
                throw new BizException("认证失败");

            string accessKeyId = Configuration["Ali:accessKeyId"];
            string accessKeySecret = Configuration["Ali:accessKeySecret"];
            string EndPoint = Configuration["Ali:EndPoint"];
            string bucketName = Configuration["Ali:bucketName"];

            if (certificationDto.UserId <= 0)
                throw new BizException("用户Id不正确");

            var user = userClient.GetUserById(certificationDto.UserId);

            if (user == null)
                throw new BizException("用户不存在");

            try
            {
                user.IdCardFrontUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                ALiOSSHelper.Upload(user.IdCardFrontUrl, certificationDto.IdCardFrontBase64, accessKeyId, accessKeySecret, EndPoint, bucketName);

                user.IdCardBackUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                ALiOSSHelper.Upload(user.IdCardBackUrl, certificationDto.IdCardBackBase64, accessKeyId, accessKeySecret, EndPoint, bucketName);

                user.DriverLicenseUrl = "User/" + Guid.NewGuid().ToString() + ".jpg";
                ALiOSSHelper.Upload(user.DriverLicenseUrl, certificationDto.DriverLicenseBase64, accessKeyId, accessKeySecret, EndPoint, bucketName);

            }
            catch (Exception e)
            {
                throw new BizException("图片上传失败" + "/" + e.Message);
            }

            user.HasAuthenticated = true;
            user.IDCard = certificationDto.IDCard;
            user.CarClass = certificationDto.CarClass;
            user.DriverBeginTime = certificationDto.DriverBeginTime;
            user.DriverEndTime = certificationDto.DriverEndTime;
            user.LastUpdateTime = DateTime.Now;

            userClient.PutUser(user);

            return Ok("认证成功");
        }
    }
}
