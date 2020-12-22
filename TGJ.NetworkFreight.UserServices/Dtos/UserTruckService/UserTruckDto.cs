using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.Dtos.UserTruckService
{
    public class UserTruckDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { set; get; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string? Color { get; set; }

        /// <summary>
        /// 车牌号 
        /// </summary>
        public string? VehicleNumber { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public Double? Length { get; set; }

        /// <summary>
        /// 板型
        /// </summary>
        public string? PlateType { get; set; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public Double? MaxWeight { get; set; }

        /// <summary>
        /// 道路运输经营许可证
        /// </summary>
        public string? BusinessLicenseUrl { get; set; }
    }
}
