using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive
{
    /// <summary>
    /// 订单成交额Dto
    /// </summary>
    public class TurnoverDto
    {
        /// <summary>
        /// 月成交额
        /// </summary>
        public decimal MonthlyTurnover { get; set; }

        /// <summary>
        /// 总成交额
        /// </summary>
        public decimal TotalTurnover { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool? HasAuthenticated { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string IdCard { get; set; }
    }
}
