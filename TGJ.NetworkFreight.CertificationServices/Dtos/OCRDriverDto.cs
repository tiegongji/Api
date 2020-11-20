using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.CertificationServices.Dtos
{
    /// <summary>
    /// 驾驶证
    /// </summary>
    public class OCRDriverDto
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string Owner { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 国家
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birth { get; set; }
        /// <summary>
        /// 准驾车型
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 初始日期
        /// </summary>
        public string Initialdate { get; set; }
        /// <summary>
        /// 开始日期 
        /// </summary>
        public string Startdate { get; set; }
        /// <summary>
        /// 结束日期 
        /// </summary>
        public string Enddate { get; set; }
        /// <summary>
        /// 档案编号
        /// </summary>
        public string Aarchiveno { get; set; }
    }
}
