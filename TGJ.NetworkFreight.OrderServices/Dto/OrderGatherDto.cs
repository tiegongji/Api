using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Dto
{
    /// <summary>
    /// 订单统计Dto
    /// </summary>
    public class OrderGatherDto
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
