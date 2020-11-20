using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 认证控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpGet("RealName")]
        public ActionResult RealNameCertification(string idCard, string name)
        {
            var dto = certificationClient.RealNameCertification(idCard, name);

            return Ok(dto);
        }
    }
}
