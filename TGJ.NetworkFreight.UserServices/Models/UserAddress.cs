﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.Models
{
    /// <summary>
    /// 用户地址模型
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { set; get; }

        /// <summary>
        /// 用户Id 
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { get; set; }

        /// <summary>
        /// 百度经度
        /// </summary>
        public float BaiduLat { get; set; }

        /// <summary>
        /// 百度纬度
        /// </summary>
        public float BaiduLng { get; set; }

        /// <summary>
        /// 高德经度
        /// </summary>
        public float GaodeLat { get; set; }

        /// <summary>
        /// 高德纬度
        /// </summary>
        public float GaodeLng { get; set; }

        /// <summary>
        /// 腾讯经度
        /// </summary>
        public float TencentLat { get; set; }

        /// <summary>
        /// 腾讯纬度
        /// </summary>
        public float TencentLng { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// 区县代码
        /// </summary>
        public string CountrySubdivisionCode { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string Province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 国家
        /// </summary>
        public string County { get; set; }

        /// <summary>
        /// 区域Id
        /// </summary>
        public int RegionID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        ///  最后修改时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}
