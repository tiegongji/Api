using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Cores.MicroClients.Attributes;
using TGJ.NetworkFreight.UserServices.Models;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Services.UserService
{
    /// <summary>
    /// 用户微服务客户端
    /// </summary>
    [MicroClient("http", "UserServices")]
    public interface IUserClient
    {
        /// <summary>
        /// 获取用户集合
        /// </summary>
        /// <returns></returns>
        [GetPath("/Users")]
        public IEnumerable<User> GetUsers();

        /// <summary>
        /// 获取用户
        /// </summary>
        /// <returns></returns>
        //[GetPath("/Users/{id}")]
        //public User GetUser(int id);

        /// <summary>
        /// 用户注册
        /// </summary>
        /// <returns></returns>
        [PostPath("/Users")]
        public User PostUser(User User);

        /// <summary>
        /// 用户更新
        /// </summary>
        /// <returns></returns>
        [PostPath("/Users/Update")]
        public void PutUser(User User);

        /// <summary>
        /// 根据OpenId获取用户
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [GetPath("/Users/{openId}")]
        public User GetUserByOpenId(string openId);

        /// <summary>
        /// 根据Id获取用户
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [GetPath("/Users/Id/{id}")]
        public User GetUserById(int id);

        /// <summary>
        /// 迷糊查询
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [GetPath("/Users/Key/{key}")]
        public IEnumerable<User>  GetUserByKey(string key);
    }
}
