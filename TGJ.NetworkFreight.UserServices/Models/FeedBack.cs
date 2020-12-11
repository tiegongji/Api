using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.Models
{
    /// <summary>
    /// 意见反馈
    /// </summary>
    public class FeedBack
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
        /// 内容
        /// </summary>
        public string Remark { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { set; get; }

    }
}
