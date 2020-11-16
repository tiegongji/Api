using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.CertificationServices.Dtos
{
    /// <summary>
    /// 实名认证Dto
    /// </summary>
    public class RealNameDto
    {
        /// <summary>
        /// 状态码:详见状态码说明 01 通过，02不通过
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 提示信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdCard { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 县
        /// </summary>
        public string Prefecture { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// AddrCode
        /// </summary>
        public string AddrCode { get; set; }

        /// <summary>
        /// LastCode
        /// </summary>
        public string LastCode { get; set; }
    }
}
