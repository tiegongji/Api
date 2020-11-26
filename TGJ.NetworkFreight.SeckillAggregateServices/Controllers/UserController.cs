using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.Cores.DynamicMiddleware.Urls;
using TGJ.NetworkFreight.SeckillAggregateServices.Dtos.UserService;
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
    [Route("api/User")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserClient userClient;
        private readonly IDynamicMiddleUrl dynamicMiddleUrl; // 中台url
        private readonly HttpClient httpClient;

        public UserController(IUserClient userClient, IDynamicMiddleUrl dynamicMiddleUrl
                                , IHttpClientFactory httpClientFactory)
        {
            this.userClient = userClient;
            this.dynamicMiddleUrl = dynamicMiddleUrl;
            this.httpClient = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// 获取用户集合
        /// </summary>
        /// <returns></returns>

        [HttpGet]
        public ActionResult<User> GetUser()
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
        /// 获取微信信息
        /// </summary>
        /// <param name="wXLoginPo"></param>
        /// <returns></returns>
        [HttpPost("WXUserInfo")]
        public WechatUserInfo GetWXUserInfo(WXLoginPo wXLoginPo)
        {
            var wli = new WechatLoginInfo();
            wli.code = wXLoginPo.code;
            wli.encryptedData = wXLoginPo.encryptedData;
            wli.iv = wXLoginPo.iv;
            wli.rawData = wXLoginPo.rawData;
            wli.signature = wXLoginPo.signature;

            WechatUserInfo wechatResult = new WeChatAppDecrypt().Decrypt(wli);

            if (wechatResult == null || string.IsNullOrWhiteSpace(wechatResult.openId))
            {
                throw new BizException("用户信息获取失败");
            }

            return wechatResult;
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

            if (wechatResult == null || string.IsNullOrWhiteSpace(wechatResult.phoneNumber))
            {
                throw new BizException("手机号获取失败");
            }

            var userInfo = userClient.GetUserByOpenId(wechatResult.openId);

            if (null == userInfo || userInfo.Id <= 0)
            {
                var model = new User()
                {
                    CreateTime = DateTime.Now,
                    Phone = wechatResult.phoneNumber,
                    wx_HeadImgUrl = loginPo.AvatarUrl,
                    wx_NickName = loginPo.NickName,
                    Name = loginPo.NickName,
                    wx_OpenID = wechatResult.openId,
                    wx_UnionID = wechatResult.unionId,
                    HasAuthenticated = false,
                    RoleName =Convert.ToInt32(loginPo.RoleName),
                    Status = 1
                };
                var obj = userClient.PostUser(model);

                if (obj == null || obj.Id <= 0)
                {
                    throw new BizException("用户新增失败");
                }
                else
                {
                    userInfo = obj;
                }
            }
            else
            {
                userInfo.wx_HeadImgUrl = loginPo.AvatarUrl;
                userInfo.wx_NickName = loginPo.NickName;
                userInfo.LastUpdateTime = DateTime.Now;

                userClient.PutUser(userInfo);
            }

            // 1、获取IdentityServer接口文档
            string userUrl = dynamicMiddleUrl.GetMiddleUrl("http", "UserServices");

            DiscoveryDocumentResponse discoveryDocument = httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest { Address = userUrl, Policy = new DiscoveryPolicy { RequireHttps = false } }).Result;

            //discoveryDocument.Policy.RequireHttps = false;


            if (discoveryDocument.IsError)
            {
                throw new BizException($"[DiscoveryDocumentResponse Error]: {discoveryDocument.Error}");
            }

            // 2、根据用户名和密码建立token
            TokenResponse tokenResponse = httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,
                ClientId = "client-password",
                ClientSecret = "secret",
                GrantType = "password",
                //Scope = "TGJService",
                UserName = userInfo.Id.ToString(),
                Password = wechatResult.phoneNumber
                //UserName = "12",
                //Password = "13636572806"
            }).Result;

            // 3、返回AccessToken
            if (tokenResponse.IsError)
            {
                throw new BizException(discoveryDocument.TokenEndpoint + "/" + userInfo.Id.ToString() + "/" + wechatResult.phoneNumber + "/" + tokenResponse.Error + "," + tokenResponse.Raw);
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
            userDto.UserName = userInfo.Name;
            userDto.AccessToken = tokenResponse.AccessToken;
            userDto.ExpiresIn = tokenResponse.ExpiresIn;

            return userDto;
        }
    }
}