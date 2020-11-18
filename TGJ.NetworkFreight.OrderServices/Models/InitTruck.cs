using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    /// <summary>
    /// 卡车型号表
    /// </summary>
    public class InitTruck
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int ID { set; get; }
        /// <summary>
        /// 车长长度
        /// </summary>
        public float Length { set; get; }
        /// <summary>
        /// 最大载重
        /// </summary>
        public float MaxWeight { set; get; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public int IsValid { set; get; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateTime { set; get; }
    }
}
