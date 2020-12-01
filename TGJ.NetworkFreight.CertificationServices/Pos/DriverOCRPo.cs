using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.CertificationServices.Pos
{
    /// <summary>
    /// DriverOCRPo
    /// </summary>
    public class DriverOCRPo : OCRPo
    {
        /// <summary>
        /// 1:正面/2:反面
        /// </summary>
        public string Type { get; set; }
    }
}
