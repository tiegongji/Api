using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.UserServices.WeChat.Lib;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserService
{
    /// <summary>
    /// 登录表单
    /// </summary>
    public class LoginPo : WXLoginPo
    {
        /// <summary>
        /// 角色 1：物流端/2：司机端
        /// </summary>
        public int RoleName { set; get; }

        public string NickName { get; set; }

        public string AvatarUrl { get; set; }
    }
}
