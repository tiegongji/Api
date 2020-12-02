using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserTruckService
{
    /// <summary>
    /// 许可证信息参数
    /// </summary>
    public class BusinessLicensePo
    {
        /// <summary>
        /// 车辆id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 道路运输证
        /// </summary>
        public string? TransportLicenseUrl { get; set; }

        /// <summary>
        /// 道路运输经营许可证
        /// </summary>
        public string? BusinessLicenseUrl { get; set; }

        /// <summary>
        /// 道路运输证书编号
        /// </summary>
        public string? RoadTransportCertificateNumber { get; set; }

        /// <summary>
        /// 道路运输管理编号
        /// </summary>
        public string? RoadTransportManageNumber { get; set; }
    }
}
