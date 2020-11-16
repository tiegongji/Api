using System;
using System.Collections.Generic;
using System.Text;

namespace TGJ.NetworkFreight.Cores.MicroClients.Attributes
{
    /// <summary>
    /// 微服务客户端特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public class MicroClient : Attribute
    {

        public string UrlShcme { get; }
        public string ServiceName { get; }

        public MicroClient(string urlShcme, string serviceName)
        {
            UrlShcme = urlShcme;
            ServiceName = serviceName;
        }
    }
}
