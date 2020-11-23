using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.OrderServices.Models.Enum
{
    public class EnumHelper
    {
        /// <summary>
        /// 1：接单中
        /// 2：已接单
        /// 3：已开工
        /// </summary>
        public enum EnumOrderStatus
        {
            [Description("接单中")]
            Publishing = 1,
            [Description("已接单")]
            Received = 2,
            [Description("已开工")]
            Start = 1,
        }
    }
}
