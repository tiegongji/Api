using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserService
{
    /// <summary>
    /// 获取微信用户
    /// </summary>
    public class WXLoginPo
    {
        /// <summary>
        /// code
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// encryptedData
        /// </summary>
        public string encryptedData { get; set; }
        /// <summary>
        /// iv
        /// </summary>
        public string iv { get; set; }
        /// <summary>
        /// rawData
        /// </summary>
        public string rawData { get; set; }
        /// <summary>
        /// signature
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 1:物流端/2:司机端
        /// </summary>
        public int RoleName { get; set; }
    }
}
