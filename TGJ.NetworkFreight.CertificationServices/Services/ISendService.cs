using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.CertificationServices.Services
{
    /// <summary>
    /// 消息推送服务接口
    /// </summary>
    public interface ISendService
    {
        SendSmsResponse SendSms(string phone, string code);
    }
}
