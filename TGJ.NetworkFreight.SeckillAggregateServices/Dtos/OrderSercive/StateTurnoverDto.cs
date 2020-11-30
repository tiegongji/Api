using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive
{
    /// <summary>
    /// 司机端订单金额统计
    /// </summary>
    public class StateTurnoverDto
    {
        /// <summary>
        /// 调度中金额
        /// </summary>
        public decimal DispatchTurnover { get; set; }

        /// <summary>
        /// 已完成金额
        /// </summary>
        public decimal CompleteTurnover { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool? HasAuthenticated { get; set; }
    }
}
