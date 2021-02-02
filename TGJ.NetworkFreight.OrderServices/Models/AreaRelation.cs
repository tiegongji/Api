using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    public class AreaRelation
    {
        [Key]
        public int? ID { get; set; }
        public string OrderNo { get; set; }
        public int? Type { get; set; }
        public string RelationOrderNo { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
       
    }
}
