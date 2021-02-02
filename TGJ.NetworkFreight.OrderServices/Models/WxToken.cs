using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    public class WxToken
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID { set; get; }
        /// <summary>
        /// 
        /// </summary>
        public int Type { set; get; }
        /// <summary>
        /// toen
        /// </summary>
        public string AppId { set; get; }
        /// <summary>
        /// toen
        /// </summary>
        public string Secret { set; get; }
        /// <summary>
        /// toen
        /// </summary>
        public string Token { set; get; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime UpdateTime { set; get; }
        /// <summary>
        /// toen
        /// </summary>
        public string Template_id { set; get; }
    }
}
