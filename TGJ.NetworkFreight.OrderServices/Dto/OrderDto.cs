using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.OrderServices.Models;

namespace TGJ.NetworkFreight.OrderServices.Dto
{
    public class OrderDto
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        public int ID { set; get; }

        /// <summary>
        /// 图片集合
        /// </summary>

        public List<OrderReceiptImage> imgs { set; get; }
    }
}
