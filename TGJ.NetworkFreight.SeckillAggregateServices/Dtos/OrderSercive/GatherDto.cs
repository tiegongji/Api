using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive
{
    /// <summary>
    /// 首页订单统计
    /// </summary>
    public class GatherDto
    {
        /// <summary>
        /// 调度
        /// </summary>
        public int Dispatch { get; set; }
        /// <summary>
        /// 确认
        /// </summary>
        public int Confirm { get; set; }
        /// <summary>
        /// 完成
        /// </summary>
        public int Complete { get; set; }
    }
}
