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
        [GetPath("/Certifications/IdCard/Certification")]
        public object RealNameCertification(string idCard, string name);

        /// <summary>
        /// 身份证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        [GetPath("/Certifications/IdCard/OCR")]
        public object OCRIdCard(string image, string side);

        /// <summary>
        /// 银行卡OCR
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Bank/OCR")]
        public object OCRBank(string image);

        /// <summary>
        /// 银行卡认证
        /// </summary>
        /// <param name="backCard"></param>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Bank/Certification")]
        public object BankCertification(string backCard, string idCard, string name);

        /// <summary>
        /// 驾驶证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Driver/OCR")]
        public object OCRDriver(string image, string type);


        /// <summary>
        /// 行驶证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Vehicle/OCR")]
        public object OCRVehicle(string image, string type);

        /// <summary>
        /// 道路经营许可证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Permit/OCR")]
        public object OCRPermit(string image, string type);
    }
}
