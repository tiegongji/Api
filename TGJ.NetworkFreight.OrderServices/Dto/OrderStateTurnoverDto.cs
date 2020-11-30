using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Dto
{
    /// <summary>
    /// 司机端我的Dto
    /// </summary>
    public class OrderStateTurnoverDto
    {
        /// <summary>
        /// 调度中金额
        /// </summary>
        public decimal DispatchTurnover { get; set; }

        /// <summary>
        /// 已完成金额
        /// </summary>
        public decimal CompleteTurnover { get; set; }
    }
}
