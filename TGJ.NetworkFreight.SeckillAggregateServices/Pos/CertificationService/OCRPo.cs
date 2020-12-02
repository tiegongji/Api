using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.CertificationService
{
    /// <summary>
    /// OCR入参
    /// </summary>
    public class OCRPo: OCRBasePo
    {
        /// <summary>
        /// 1:正面/2:反面
        /// </summary>
        public string Type { get; set; }
    }
}
