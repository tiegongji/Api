using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.OrderSercive
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderPo
    {/// <summary>
     /// 订单ID
     /// </summary>
        public string OrderNo { set; get; }

        public int UserID { set; get; }

        public List<OrderReceiptImage> imgs { set; get; }
    }
}
