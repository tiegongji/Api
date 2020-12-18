using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    /// <summary>
    /// 订单表
    /// </summary>
    public class Order
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserID { set; get; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string OrderNo { set; get; }

        /// <summary>
        /// 订单状态
        /// </summary>
        public int TradeStatus { set; get; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ActionStatus { set; get; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal TotalAmount { set; get; }
        /// <summary>
        /// 承运人ID
        /// </summary>
        public int CarrierUserID { set; get; }
        /// <summary>
        /// 承运人车辆ID
        /// </summary>
        public int CarrierTruckID { set; get; }
        /// <summary>
        /// 装货时间
        /// </summary>
        public DateTime? DepartureTime { set; get; }
        /// <summary>
        /// 到达时间
        /// </summary>
        public DateTime? ArrivalTime { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LastUpdateTime { set; get; }
    }
}
