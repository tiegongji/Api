using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Dtos.OrderSercive
{
    public class OrderDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int ID { set; get; }

        public int UserID { set; get; }

        public List<OrderReceiptImage> imgs { set; get; }
    }
}
