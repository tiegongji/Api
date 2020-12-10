using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Dtos.UserService
{
    /// <summary>
    /// 用户登录成功返回的信息
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// Token
        /// </summary>
        public string AccessToken { set; get; }
        /// <summary>
        /// <summary>
        /// RefreshToken
        /// </summary>
        public string RefreshToken { set; get; }
        /// <summary>
        /// AccessToken过期时间
        /// </summary>
        public int ExpiresIn { set; get; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { set; get; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { set; get; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public bool? HasAuthenticated { get; set; }
    }
}
