using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.Models
{
    /// <summary>
    /// 文件存储
    /// </summary>
    public class UpLoadFile
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
        ///  类型
        /// </summary>
        public int Type { set; get; }
        /// <summary>
        /// 关联ID
        /// </summary>
        public int TypeID { set; get; }
        public int Status { set; get; }
        /// <summary>
        ///  文件路径
        /// </summary>
        public string FilePath { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { set; get; }
    }
}
