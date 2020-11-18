using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    /// <summary>
    /// 订单流程表
    /// </summary>
    public class OrderFlow
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 动作状态
        /// </summary>
        public int ActionStatus { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
