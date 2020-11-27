using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.UserServices.WeChat.Lib
{
    /// <summary>
    /// 微信小程序登录信息结构
    /// </summary>
    public class WechatLoginInfo
    {
        public string code { get; set; }
        public string encryptedData { get; set; }
        public string iv { get; set; }
        public string rawData { get; set; }
        public string signature { get; set; }
        public string appid { get; set; }
        public string secret { get; set; }
    }
}
