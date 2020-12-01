using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.CertificationServices.Pos
{
    /// <summary>
    /// IdCardOCRDto
    /// </summary>
    public class IdCardOCRPo: OCRPo
    {
        /// <summary>
        /// front：身份证带人脸一面，back：身份证带国徽片一面
        /// </summary>

        public string Side { get; set; }
    }
}
