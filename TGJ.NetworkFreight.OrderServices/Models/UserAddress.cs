using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models
{
    /// <summary>
    /// 地址表
    /// </summary>
    public class UserAddress
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
        /// 项目名称
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 联系人
        /// </summary>
        public string ContactPerson { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string ContactPhone { set; get; }
        /// <summary>
        /// 百度纬度
        /// </summary>
        public Single BaiduLat { set; get; }
        /// <summary>
        /// 百度经度
        /// </summary>
        public Single BaiduLng { set; get; }
        /// <summary>
        /// 高德纬度
        /// </summary>
        public Single GaodeLat { set; get; }
        /// <summary>
        /// 高德经度
        /// </summary>
        public Single GaodeLng { set; get; }
        /// <summary>
        /// 腾讯纬度
        /// </summary>
        public Single TencentLat { set; get; }
        /// <summary>
        /// 腾讯经度
        /// </summary>
        public Single TencentLng { set; get; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Address { set; get; }
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool IsValid { set; get; }
        /// <summary>
        /// 国家行政区代码
        /// </summary>
        public string CountrySubdivisionCode { set; get; }
        /// <summary>
        /// 省
        /// </summary>
        public string Province { set; get; }
        /// <summary>
        /// 市
        /// </summary>
        public string City { set; get; }
        /// <summary>
        /// 区
        /// </summary>
        public string County { set; get; }
        /// <summary>
        /// 县ID
        /// </summary>
        public string RegionID { set; get; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { set; get; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LastUpdateTime { set; get; }

    }
}
