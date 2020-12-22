using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.OrderSercive
{
    public class OrderImgFormDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public string OrderNo { set; get; }

        public int UserID { set; get; }

        public string imgs { set; get; }
    }
}
