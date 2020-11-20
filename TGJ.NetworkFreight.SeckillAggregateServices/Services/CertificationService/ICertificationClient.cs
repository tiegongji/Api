using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.CertificationService
{
    /// <summary>
    /// 认证客户端
    /// </summary>
    [MicroClient("http", "CertificationServices")]
    public interface ICertificationClient
    {
        /// <summary>
        /// 身份证实名
        /// </summary>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [GetPath("/Certification")]
        public object RealNameCertification(string idCard, string name);
    }
}
