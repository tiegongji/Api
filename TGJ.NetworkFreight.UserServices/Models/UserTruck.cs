using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.Models
{
    /// <summary>
    /// 用户车辆模型
    /// </summary>
    public class UserTruck
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
        /// 车牌号 
        /// </summary>
        public string VehicleNumber { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// 长度
        /// </summary>
        public float Length { get; set; }

        /// <summary>
        /// 板型
        /// </summary>
        public string PlateType { get; set; }

        /// <summary>
        /// 车主
        /// </summary>
        public string Owner { get; set; }

        /// <summary>
        /// 最大宽度
        /// </summary>
        public string MaxWeight { get; set; }

        /// <summary>
        /// 注册日期
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime PresentationDate { get; set; }

        /// <summary>
        /// 道路运输证书编号
        /// </summary>
        public string RoadTransportCertificateNumber { get; set; }

        /// <summary>
        /// 道路运输管理编号
        /// </summary>
        public string RoadTransportManageNumber { get; set; }

        /// <summary>
        /// 是否有驾照
        /// </summary>
        public bool HasDrivingLicense { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public string IsValid { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
