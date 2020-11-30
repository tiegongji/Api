using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models.Enum
{
    public class EnumHelper
    {
        public enum EnumOrderStatus
        {
            /// <summary>
            /// 已取消
            /// </summary>
            [Description("已取消")]
            Cancel = -1,
            /// <summary>
            /// 接单中
            /// </summary>
            [Description("接单中")]
            Waiting = 1,
            /// <summary>
            /// 已接单
            /// </summary>
            [Description("已接单")]
            Received = 2,
            /// <summary>
            /// 调度中
            /// </summary>
            [Description("调度中")]
            Start = 3,
            /// <summary>
            /// 已完成
            /// </summary>
            [Description("已完成")]
            Finish = 4,
        }

        public enum EnumActionStatus
        {
            /// <summary>
            /// 已支付
            /// </summary>
            [Description("已支付")]
            Pay = 1,
            /// <summary>
            /// 已装货
            /// </summary>
            [Description("已装货")]
            Loading = 2,
            /// <summary>
            /// 已卸货
            /// </summary>
            [Description("已卸货")]
            Unloading = 3,
        }

        /// <summary>
        /// 司机端列表状态描述
        /// </summary>
        public enum EnumActionStatus_Driver
        {
            [Description("装货上报")]
            Pay = 1,
            [Description("卸货上报")]
            Loading = 2,
            [Description("已完成")]
            Unloading = 3,
        }

        public enum EnumType
        {
            [Description("物流端状态")]
            Logistics = 1,
            [Description("司机端状态")]
            Driver = 2
        }

        //public enum EnumImageType
        //{
        //    [Description("物流端回单确认")]
        //    Logistics = 1,
        //    [Description("司机端回单确认")]
        //    Driver = 2

        //}
    }
}
