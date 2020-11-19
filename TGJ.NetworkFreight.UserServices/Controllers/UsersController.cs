﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Services;
using TGJ.NetworkFreight.UserServices.WeChat;
using TGJ.NetworkFreight.UserServices.WeChat.Lib;

namespace TGJ.NetworkFreight.UserServices.Controllers
{
    /// <summary>
    /// 用户服务控制器
    /// </summary>
    [Route("Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService UserService;
        private readonly HttpClient httpClient;

        public UsersController(IUserService UserService, IHttpClientFactory httpClientFactory)
        {
            this.UserService = UserService;
            this.httpClient = httpClientFactory.CreateClient();
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return UserService.GetUsers().ToList();
        }

        //[HttpGet("{id}")]
        //public ActionResult<User> GetUser(int id)
        //{
        //    var User = UserService.GetUserById(id);

        //    if (User == null)
        //    {
        //        return NotFound();
        //    }

        //    return User;
        //}

        [HttpGet("{openId}")]
        public ActionResult<User> GetUserByOpenId(string openId)
        {
            var User = UserService.GetUserByOpenId(openId);

            if (User == null)
            {
                return NotFound(User);
            }

            return User;
        }

        [HttpPost]
        public ActionResult<User> PostUser(User User)
        {
            // 1、判断用户名是否重复
            //if (UserService.UserNameExists(User.Name))
            //{
            //    throw new BizException("用户名已经存在");
            //}
            UserService.Create(User);
            return CreatedAtAction("GetUser", new { id = User.Id }, User);
        }

        [HttpPost("ModifyPassword")]
        public ActionResult ModifyPassword(string phone, string password)
        {
            var user = UserService.GetUserByPhone(phone);

            if (user == null)
                return NotFound();

            user.LoginPassword = password;
            user.LastUpdateTime = DateTime.Now;

            UserService.Update(user);

            return NoContent();
        }
    }
}
