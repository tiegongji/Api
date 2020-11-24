using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService;

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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="certificationClient"></param>
        public CertificationController(ICertificationClient certificationClient)
        {
            this.certificationClient = certificationClient;
        }

        /// <summary>
        /// 身份证实名接口
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("IdCard/Certification")]
        public ActionResult RealNameCertification(string idCard, string name)
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
        public ActionResult OCRIdCard(string image, string side)
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
        public ActionResult OCRBank(string image)
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
        public ActionResult BankCertification(string backCard, string idCard, string name)
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
        public ActionResult OCRDriver(string image, string type)
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
        public ActionResult OCRVehicle(string image, string type)
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
        public ActionResult OCRPermit(string image, string type)
        {
            var dto = certificationClient.OCRPermit(image, type);

            return Ok(dto);
        }
    }
}
