using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserService
{
    /// <summary>
    /// 司机认证Dto
    /// </summary>
    public class DriverCertificationDto
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { set; get; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string? IDCard { get; set; }

        /// <summary>
        /// 准驾车型
        /// </summary>
        public string? CarClass { get; set; }

        /// <summary>
        /// 驾驶证开始时间
        /// </summary>
        public DateTime? DriverBeginTime { get; set; }
        /// <summary>

        /// <summary>
        /// 驾驶证结束时间
        /// </summary>
        public DateTime? DriverEndTime { get; set; }

        /// <summary>
        /// 身份证正面Url
        /// </summary>
        public string? IdCardFrontBase64 { set; get; }
        /// <summary>
        /// 身份证反面Url
        /// </summary>
        public string? IdCardBackBase64 { set; get; }
        /// <summary>
        /// 驾驶证Url
        /// </summary>
        public string? DriverLicenseBase64 { set; get; }
    }
}
