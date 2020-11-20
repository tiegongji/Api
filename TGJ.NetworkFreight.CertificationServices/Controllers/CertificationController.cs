﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        [HttpGet("IdCard/Certification")]
        public ActionResult<decimal> RealNameCertification(string idCard, string name)
        {
            var dto = CertificationService.RealNameCertification(idCard, name);

            return Ok(dto);

            //var json = "{\"status\":\"01\",\"msg\":\"实名认证通过！\",\"idCard\":\"61243019911018221X\",\"name\":\"汤龙\",\"sex\":\"男\",\"area\":\"陕西省安康地区白河县\",\"province\":\"陕西省\",\"city\":\"安康地区\",\"prefecture\":\"白河县\",\"birthday\":\"1991-10-18\",\"addrCode\":\"612430\",\"lastCode\":\"X\"}";

            //dynamic result = JsonConvert.DeserializeObject(json);

            //return Ok(result);
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
            var dto = CertificationService.OCRIdCard(image, side);

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
            var dto = CertificationService.OCRBank(image);

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
            var dto = CertificationService.BankCertification(backCard, idCard, name);

            return Ok(dto);
        }

        /// <summary>
        /// 驾驶证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type">1:正面/2:反面</param>
        /// <returns></returns>
        [HttpPost("Driver/OCR")]
        public ActionResult<decimal> OCRDriver(string image,string type)
        {
            var dto = CertificationService.OCRDriver(image, type);

            return Ok(dto);
        }

        /// <summary>
        /// 行驶证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost("Vehicle/OCR")]
        public ActionResult<decimal> OCRVehicle(string image, string type)
        {
            var dto = CertificationService.OCRVehicle(image, type);

            return Ok(dto);
        }

        /// <summary>
        /// 道路经营许可证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [HttpPost("Permit/OCR")]
        public ActionResult<decimal> OCRPermit(string image, string type)
        {
            var dto = CertificationService.OCRPermit(image, type);

            return Ok(dto);
        }
    }
}
