using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.CertificationService
{
    /// <summary>
    /// 身份证IdCardOCRPo入参
    /// </summary>
    public class CardOCRPo : OCRBasePo
    {
        /// <summary>
        /// front：身份证带人脸一面，back：身份证带国徽片一面
        /// </summary>

        public string Side { get; set; }
    }
}
