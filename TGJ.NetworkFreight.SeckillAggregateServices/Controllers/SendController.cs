using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.SeckillAggregateServices.MemoryCaches;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService;
using TGJ.NetworkFreight.SeckillAggregateServices.Utils;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 推送消息控制器
    /// </summary>
    [Route("api/Send")]
    [ApiController]
    [Authorize]
    public class SendController : ControllerBase
    {
        private readonly ISendClient sendClient;
        private readonly ICaching Cache;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendClient"></param>
        /// <param name="cache"></param>
        public SendController(ISendClient sendClient, ICaching cache)
        {
            this.sendClient = sendClient;
            this.Cache = cache;
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPost("Sms")]
        public ActionResult<SendSmsResponse> SendSms(string userId, string phone)
        {
            var code = Util.GetSmsCode();

            Cache.Set(userId, code);

            var response = sendClient.SendSms(phone, code);

            if (response == null)
            {
                throw new BizException("短信发送失败");
            }

            if (response.Message != "OK")
            {
                throw new BizException(response.Message);
            }

            return response;
        }
    }
}
