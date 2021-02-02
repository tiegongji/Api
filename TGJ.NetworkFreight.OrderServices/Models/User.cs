using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    /// <summary>
    /// 用户模型
    /// </summary>
    public class User
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { set; get; }

        /// <summary>
        /// 用户手机号
        /// </summary>
        public string? Phone { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 认证
        /// </summary>
        public bool? HasAuthenticated { set; get; }

        /// <summary>
        /// 名字
        /// </summary>
        public string? Name { set; get; }

        /// <summary>
        /// 角色
        /// </summary>
        public int? RoleName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string? LoginPassword { set; get; }

        /// <summary>
        /// 微信OpenId
        /// </summary>
        public string? wx_OpenID { get; set; }

        /// <summary>
        /// 微信UnionId
        /// </summary>
        public string? wx_UnionID { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public string? wx_HeadImgUrl { set; get; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        public string? wx_NickName { set; get; }

        /// <summary>
        /// 身份证
        /// </summary>
        public string? IDCard { get; set; }

        /// <summary>
        /// 车辆
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
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastUpdateTime { get; set; }

        /// <summary>
        /// 身份证正面Url
        /// </summary>
        public string? IdCardFrontUrl { set; get; }
        /// <summary>
        /// 身份证反面Url
        /// </summary>
        public string? IdCardBackUrl { set; get; }
        /// <summary>
        /// 驾驶证Url
        /// </summary>
        public string? DriverLicenseUrl { set; get; }
    }
}