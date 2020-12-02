using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserTruckService
{
    /// <summary>
    /// 用户车辆请求参数
    /// </summary>
    public class UserTruckPo
    {
        public int? Id { set; get; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public int? UserID { get; set; }

        /// <summary>
        /// 车牌号 
        /// </summary>
        public string? VehicleNumber { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public Double? Length { get; set; }

        /// <summary>
        /// 板型
        /// </summary>
        public string? PlateType { get; set; }

        /// <summary>
        /// 车主
        /// </summary>
        public string? Owner { get; set; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public Double? MaxWeight { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime? RegisterDate { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime? PresentationDate { get; set; }

        /// <summary>
        /// 道路运输证书编号
        /// </summary>
        public string? RoadTransportCertificateNumber { get; set; }

        /// <summary>
        /// 道路运输管理编号
        /// </summary>
        public string? RoadTransportManageNumber { get; set; }

        /// <summary>
        /// 是否有驾照
        /// </summary>
        public bool? HasDrivingLicense { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool? IsValid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 行驶证
        /// </summary>
        public string? TravelCardUrl { get; set; }
        /// <summary>
        /// 道路运输证
        /// </summary>
        public string? TransportLicenseUrl { get; set; }

        /// <summary>
        /// 道路运输经营许可证
        /// </summary>
        public string? BusinessLicenseUrl { get; set; }
    }
}
