using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.CertificationServices.Dtos;

namespace TGJ.NetworkFreight.CertificationServices.Services
{
    /// <summary>
    /// 认证服务接口
    /// </summary>
    public interface ICertificationService
    {
        RealNameDto RealNameCertification(string idCard, string name);

        OCRDto OCRCertification(string image, string side);
    }
}
