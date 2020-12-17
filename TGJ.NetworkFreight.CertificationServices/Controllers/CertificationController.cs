using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TGJ.NetworkFreight.CertificationServices.Dtos;
using TGJ.NetworkFreight.CertificationServices.Pos;
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
        [HttpGet("IdCard/Certification")]
        public ActionResult<dynamic> RealNameCertification(string idCard, string name)
        {
            var result = CertificationService.RealNameCertification(idCard, name);

            //var json = "{\"status\":\"01\",\"msg\":\"实名认证通过！\",\"idCard\":\"61243019911018221X\",\"name\":\"汤龙\",\"sex\":\"男\",\"area\":\"陕西省安康地区白河县\",\"province\":\"陕西省\",\"city\":\"安康地区\",\"prefecture\":\"白河县\",\"birthday\":\"1991-10-18\",\"addrCode\":\"612430\",\"lastCode\":\"X\"}";

            //dynamic result = JsonConvert.DeserializeObject(json);

            return Ok(result);
        }

        /// <summary>
        /// 身份证OCR
        /// </summary>
        /// <param name="idCardOCRDto"> </param>
        /// <returns></returns>
        [HttpPost("IdCard/OCR")]
        public ActionResult<dynamic> OCRIdCard(IdCardOCRPo idCardOCRDto)
        {
            return CertificationService.OCRIdCard(idCardOCRDto.Image, idCardOCRDto.Side);
        }

        /// <summary>
        /// 银行卡OCR
        /// </summary>
        /// <param name="oCR"></param>
        /// <returns></returns>
        [HttpPost("Bank/OCR")]
        public ActionResult<dynamic> OCRBank(OCRPo oCR)
        {
            return CertificationService.OCRBank(oCR.Image);
        }

        /// <summary>
        /// 银行卡认证
        /// </summary>
        /// <param name="backCard"></param>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("Bank/Certification")]
        public ActionResult<dynamic> BankCertification(string backCard, string idCard, string name)
        {
            return CertificationService.BankCertification(backCard, idCard, name);
        }

        /// <summary>
        /// 驾驶证OCR
        /// </summary>
        /// <param name="driverOCRPo"></param>
        /// <returns></returns>
        [HttpPost("Driver/OCR")]
        public ActionResult<dynamic> OCRDriver(DriverOCRPo driverOCRPo)
        {
            return CertificationService.OCRDriver(driverOCRPo.Image, driverOCRPo.Type);
        }

        /// <summary>
        /// 行驶证OCR
        /// </summary>
        /// <param name="driverOCRPo"></param>
        /// <returns></returns>
        [HttpPost("Vehicle/OCR")]
        public ActionResult<dynamic> OCRVehicle(DriverOCRPo driverOCRPo)
        {
            return CertificationService.OCRVehicle(driverOCRPo.Image, driverOCRPo.Type);
        }

        /// <summary>
        /// 道路经营许可证OCR
        /// </summary>
        /// <param name="driverOCRPo"></param>
        /// <returns></returns>
        [HttpPost("Permit/OCR")]
        public ActionResult<dynamic> OCRPermit(DriverOCRPo driverOCRPo)
        {
            return CertificationService.OCRPermit(driverOCRPo.Image, driverOCRPo.Type);
        }
    }
}
