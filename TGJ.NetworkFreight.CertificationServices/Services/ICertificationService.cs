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
        dynamic RealNameCertification(string idCard, string name);
        dynamic OCRIdCard(string image, string side);
        dynamic BankCertification(string bankCard, string idCard, string realName);
        dynamic OCRBank(string pic);
        dynamic OCRDriver(string pic, string type);
        dynamic OCRVehicle(string pic, string type);
        dynamic OCRPermit(string pic, string type);
    }
}
