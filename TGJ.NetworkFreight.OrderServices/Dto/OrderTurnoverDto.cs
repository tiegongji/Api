using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Dto
{
    /// <summary>
    /// 订单金额 
    /// </summary>
    public class OrderTurnoverDto
    {
        /// <summary>
        /// 月成交额
        /// </summary>
        public decimal MonthlyTurnover { get; set; }

        /// <summary>
        /// 总成交额
        /// </summary>
        public decimal TotalTurnover { get; set; }
    }
}
