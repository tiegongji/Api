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
        decimal RealNameCertification(string idCard, string name);
        decimal OCRIdCard(string image, string side);
        decimal BankCertification(string bankCard, string idCard, string realName);
        decimal OCRBank(string pic);
        decimal OCRDriver(string pic, string type);
        decimal OCRVehicle(string pic, string type);
        decimal OCRPermit(string pic, string type);
    }
}
