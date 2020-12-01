using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.CertificationService;

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
        public dynamic RealNameCertification(string idCard, string name);

        /// <summary>
        /// 身份证OCR
        /// </summary>
        /// <param name="cardOCRPo"></param>
        /// <returns></returns>
        [PostPath("/Certifications/IdCard/OCR")]
        public dynamic OCRIdCard(CardOCRPo cardOCRPo);

        /// <summary>
        /// 银行卡OCR
        /// </summary>
        /// <param name="oCR"></param>
        /// <returns></returns>
        [PostPath("/Certifications/Bank/OCR")]
        public dynamic OCRBank(OCRBasePo oCR);

        /// <summary>
        /// 银行卡认证
        /// </summary>
        /// <param name="backCard"></param>
        /// <param name="idCard"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [GetPath("/Certifications/Bank/Certification")]
        public dynamic BankCertification(string backCard, string idCard, string name);

        /// <summary>
        /// 驾驶证OCR
        /// </summary>
        /// <param name="oCR"></param>
        /// <returns></returns>
        [PostPath("/Certifications/Driver/OCR")]
        public dynamic OCRDriver(OCRPo oCR);


        /// <summary>
        /// 行驶证OCR
        /// </summary>
        /// <param name="oCR"></param>
        /// <returns></returns>
        [PostPath("/Certifications/Vehicle/OCR")]
        public dynamic OCRVehicle(OCRPo oCR);

        /// <summary>
        /// 道路经营许可证OCR
        /// </summary>
        /// <param name="oCR"></param>
        /// <returns></returns>
        [PostPath("/Certifications/Permit/OCR")]
        public dynamic OCRPermit(OCRPo oCR);
    }
}
