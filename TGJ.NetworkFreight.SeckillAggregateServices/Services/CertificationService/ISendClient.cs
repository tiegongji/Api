using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService
{
    /// <summary>
    /// 推送服务客户端
    /// </summary>
    [MicroClient("http", "CertificationServices")]
    public interface ISendClient
    {
        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [PostPath("/Send/Sms")]
        public SendSmsResponse SendSms(string phone, string code);
    }
}
