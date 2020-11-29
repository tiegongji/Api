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
        public decimal RealNameCertification(string idCard, string name);

        /// <summary>
        /// 身份证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="side"></param>
        /// <returns></returns>
        [PostPath("/Certifications/IdCard/OCR")]
        public decimal OCRIdCard(string image, string side);

        /// <summary>
        /// 银行卡OCR
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
        [PostPath("/Certifications/Bank/OCR")]
        public decimal OCRBank(string image);

        /// <summary>
        /// 银行卡认证
        /// </summary>
        /// <param name="backCard"></param>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Bank/Certification")]
        public decimal BankCertification(string backCard, string idCard, string name);

        /// <summary>
        /// 驾驶证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [PostPath("/Certifications/Driver/OCR")]
        public decimal OCRDriver(string image, string type);


        /// <summary>
        /// 行驶证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Vehicle/OCR")]
        public decimal OCRVehicle(string image, string type);

        /// <summary>
        /// 道路经营许可证OCR
        /// </summary>
        /// <param name="image"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Permit/OCR")]
        public decimal OCRPermit(string image, string type);
    }
}
