using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.CertificationServices.Dtos;
using TGJ.NetworkFreight.CertificationServices.Services;

namespace TGJ.NetworkFreight.CertificationServices.Controllers
{
    /// <summary>
    /// 认证控制器
    /// </summary>
    [Route("Certifications")]
    [ApiController]
    public class CertificationController : ControllerBase
    {
        private readonly ICertificationService CertificationService;

        public CertificationController(ICertificationService certificationService)
        {
            CertificationService = certificationService;
        }

        /// <summary>
        /// 身份证实名认证
        /// </summary>
        /// <param name="idCard">身份证号码</param>
        /// <param name="name">姓名</param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<RealNameDto> RealNameCertification(string idCard, string name)
        {
            return CertificationService.RealNameCertification(idCard, name);
        }

        /// <summary>
        /// 身份证OCR扫描
        /// </summary>
        /// <param name="image">不包含图片头的，如data:image/jpg;base64,) </param>
        /// <param name="side">front：身份证带人脸一面，back：身份证带国徽片一面</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<OCRDto> OCRCertification(string image, string side)
        {
            return CertificationService.OCRCertification(image, side);
        }
    }
}
