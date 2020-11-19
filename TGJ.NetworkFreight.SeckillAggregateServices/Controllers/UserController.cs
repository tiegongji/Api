using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.Cores.DynamicMiddleware.Urls;
using TGJ.NetworkFreight.SeckillAggregateServices.Dtos.UserService;
using TGJ.NetworkFreight.SeckillAggregateServices.MemoryCaches;
using TGJ.NetworkFreight.SeckillAggregateServices.Pos.UserService;
using TGJ.NetworkFreight.SeckillAggregateServices.Services.UserService;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.WeChat;
using TGJ.NetworkFreight.UserServices.WeChat.Lib;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Controllers
{
    /// <summary>
    /// 用户控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserClient userClient;
        private readonly IDynamicMiddleUrl dynamicMiddleUrl; // 中台url
        private readonly HttpClient httpClient;
        private readonly ICaching Cache;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userClient"></param>
        /// <param name="dynamicMiddleUrl"></param>
        /// <param name="httpClientFactory"></param>
        public UserController(IUserClient userClient, IDynamicMiddleUrl dynamicMiddleUrl
                                , IHttpClientFactory httpClientFactory, ICaching cache)
        {
            this.userClient = userClient;
            this.dynamicMiddleUrl = dynamicMiddleUrl;
            this.httpClient = httpClientFactory.CreateClient();
            this.Cache = cache;
        }

        /// <summary>
        /// 获取用户集合
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {
            //    var model = new User()
            //    {
            //        CreateTime = DateTime.Now,
            //        Phone = "ss",
            //        wx_HeadImgUrl = "ss",
            //        wx_NickName = "11111",
            //        wx_OpenID = "eeeeeee",
            //        wx_UnionID = "ss",
            //        HasAuthenticated = false,
            //        Name = "哇哇哇我哇",
            //        Status = 1
            //    };
            var obj = userClient.GetUsers();

            return Ok(obj);
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpPost("Login")]
        public UserDto PostLogin(LoginPo loginPo)
        {
            var wli = new WechatLoginInfo();
            wli.code = loginPo.code;
            wli.encryptedData = loginPo.encryptedData;
            wli.iv = loginPo.iv;
            wli.rawData = loginPo.rawData;
            wli.signature = loginPo.signature;

            WechatUserInfo wechatResult = new WeChatAppDecrypt().Decrypt(wli);

            if (wechatResult == null || string.IsNullOrWhiteSpace(wechatResult.openId))
            {
                throw new BizException("授权失败");
            }

            wechatResult.nickName = HttpUtility.UrlEncode(wechatResult.nickName);

            var userInfo = userClient.GetUserByOpenId(wechatResult.openId);

            var userid = 0;

            if (null == userInfo || userInfo.Id <= 0)
            {
                var model = new User()
                {
                    CreateTime = DateTime.Now,
                    Phone = wechatResult.phoneNumber,
                    wx_HeadImgUrl = wechatResult.avatarUrl,
                    wx_NickName = wechatResult.nickName,
                    wx_OpenID = wechatResult.openId,
                    wx_UnionID = wechatResult.unionId,
                    HasAuthenticated = false,
                    RoleName = "1",
                    Status = 1
                };
                var obj = userClient.PostUser(model);

                if (obj == null || obj.Id <= 0)
                {
                    throw new BizException("用户新增失败");
                }
                else
                {
                    userid = obj.Id;
                }
            }
            else
            {
                userid = userInfo.Id;
            }

            // 1、获取IdentityServer接口文档
            string userUrl = dynamicMiddleUrl.GetMiddleUrl("https", "UserServices");

            DiscoveryDocumentResponse discoveryDocument = httpClient.GetDiscoveryDocumentAsync(userUrl).Result;

            if (discoveryDocument.IsError)
            {
                Console.WriteLine($"[DiscoveryDocumentResponse Error]: {discoveryDocument.Error}");
            }

            // 2、根据用户名和密码建立token
            TokenResponse tokenResponse = httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client-password",
                ClientSecret = "secret",
                GrantType = "password",
                UserName = userid.ToString(),
                Password = userInfo.Phone
            }).Result;

            // 3、返回AccessToken
            if (tokenResponse.IsError)
            {
                throw new BizException(tokenResponse.Error + "," + tokenResponse.Raw);
            }

            // 4、获取用户信息
            UserInfoResponse userInfoResponse = httpClient.GetUserInfoAsync(new UserInfoRequest()
            {
                Address = discoveryDocument.UserInfoEndpoint,
                Token = tokenResponse.AccessToken
            }).Result;

            // 5、返回UserDto信息
            UserDto userDto = new UserDto();
            userDto.UserId = userInfoResponse.Json.TryGetString("sub");
            userDto.UserName = loginPo.UserName;
            userDto.AccessToken = tokenResponse.AccessToken;
            userDto.ExpiresIn = tokenResponse.ExpiresIn;

            return userDto;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="passwordPo"></param>
        /// <returns></returns>
        [HttpPost("ModifyPassword")]
        public ActionResult ModifyPassword(ModifyPasswordPo passwordPo)
        {
            var code = Cache.Get(passwordPo.UserId.ToString());

            if (code == null)
                throw new BizException("验证码已过期");

            if (code.Equals(passwordPo.Code))
                throw new BizException("验证码不正确");

            userClient.ModifyPassword(passwordPo.Phone, passwordPo.Password);

            return NoContent();
        }
    }
}