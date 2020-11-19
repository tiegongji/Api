﻿using System;
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
        /// 根据OpenId获取用户
        /// </summary>
        /// <param name="openId"></param>
        /// <returns></returns>
        [GetPath("/Users/{openId}")]
        public User GetUserByOpenId(string openId);

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="password"></param>
        [PostPath("/Users/ModifyPassword")]
        public void ModifyPassword(string phone, string password);
    }
}
