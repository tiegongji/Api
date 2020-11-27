using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    public class OrderCancelDto
    {
        public int UserID { get; set; }
        public string OrderNo { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
    }
}
