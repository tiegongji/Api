using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.CertificationServices.Services;

namespace TGJ.NetworkFreight.CertificationServices.Controllers
{
    /// <summary>
    /// 消息推送控制器
    /// </summary>
    [Route("Send")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly ISendService SendService;

        public SendController(ISendService sendService)
        {
            SendService = sendService;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Sms")]
        public ActionResult<SendSmsResponse> SendSms(string phone, string code)
        {
            return SendService.SendSms(phone, code);
        }
    }
}
