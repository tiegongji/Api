using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserService
{
    /// <summary>
    /// 登录表单
    /// </summary>
    public class LoginPo
    {
        public string UserName { set; get; } // 用户名
        public string Password { set; get; }// 密码 

        public string code { get; set; }
        public string encryptedData { get; set; }
        public string iv { get; set; }
        public string rawData { get; set; }
        public string signature { get; set; }
    }
}
