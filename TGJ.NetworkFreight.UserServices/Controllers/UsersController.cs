using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using IdentityModel.Client;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.Cores.DynamicMiddleware.Urls;
using TGJ.NetworkFreight.UserServices.Dtos.UserService;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Pos.UserService;
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
        private readonly IDynamicMiddleUrl dynamicMiddleUrl; // 中台url
        private readonly HttpClient httpClient;

        public UsersController(IUserService UserService, IDynamicMiddleUrl dynamicMiddleUrl, IHttpClientFactory httpClientFactory)
        {
            this.UserService = UserService;
            this.dynamicMiddleUrl = dynamicMiddleUrl;
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


        ///// <summary>
        ///// 用户登录
        ///// </summary>
        ///// <returns></returns>
        //[HttpPost("Login")]
        //public UserDto PostLogin(LoginPo loginPo)
        //{
        //    var wli = new WechatLoginInfo();
        //    wli.code = loginPo.code;
        //    wli.encryptedData = loginPo.encryptedData;
        //    wli.iv = loginPo.iv;
        //    wli.rawData = loginPo.rawData;
        //    wli.signature = loginPo.signature;

        //    WechatUserInfo wechatResult = new WeChatAppDecrypt().Decrypt(wli);

        //    if (wechatResult == null || string.IsNullOrWhiteSpace(wechatResult.openId))
        //    {
        //        throw new BizException("授权失败");
        //    }

        //    wechatResult.nickName = HttpUtility.UrlEncode(wechatResult.nickName);

        //    var userInfo = UserService.GetUserByOpenId(wechatResult.openId);

        //    var userid = 0;

        //    if (null == userInfo || userInfo.Id <= 0)
        //    {
        //        var model = new User()
        //        {
        //            CreateTime = DateTime.Now,
        //            Phone = wechatResult.phoneNumber,
        //            wx_HeadImgUrl = wechatResult.avatarUrl,
        //            wx_NickName = wechatResult.nickName,
        //            wx_OpenID = wechatResult.openId,
        //            wx_UnionID = wechatResult.unionId,
        //            HasAuthenticated = false,
        //            RoleName = "1",
        //            Status = 1
        //        };
        //        var obj = UserService.Create(model);

        //        if (obj == null || obj.Id <= 0)
        //        {
        //            throw new BizException("用户新增失败");
        //        }
        //        else
        //        {
        //            userid = obj.Id;
        //        }
        //    }
        //    else
        //    {
        //        userid = userInfo.Id;
        //    }

        //    // 1、获取IdentityServer接口文档
        //    string userUrl = dynamicMiddleUrl.GetMiddleUrl("https", "UserServices");

        //    DiscoveryDocumentResponse discoveryDocument = httpClient.GetDiscoveryDocumentAsync(userUrl).Result;

        //    if (discoveryDocument.IsError)
        //    {
        //        Console.WriteLine($"[DiscoveryDocumentResponse Error]: {discoveryDocument.Error}");
        //    }

        //    // 2、根据用户名和密码建立token
        //    TokenResponse tokenResponse = httpClient.RequestPasswordTokenAsync(new PasswordTokenRequest()
        //    {
        //        Address = discoveryDocument.TokenEndpoint,
        //        ClientId = "client-password",
        //        ClientSecret = "secret",
        //        GrantType = "password",
        //        UserName = userid.ToString(),
        //        Password = userInfo.Phone
        //    }).Result;

        //    // 3、返回AccessToken
        //    if (tokenResponse.IsError)
        //    {
        //        throw new BizException(tokenResponse.Error + "," + tokenResponse.Raw);
        //    }

        //    // 4、获取用户信息
        //    UserInfoResponse userInfoResponse = httpClient.GetUserInfoAsync(new UserInfoRequest()
        //    {
        //        Address = discoveryDocument.UserInfoEndpoint,
        //        Token = tokenResponse.AccessToken
        //    }).Result;

        //    // 5、返回UserDto信息
        //    UserDto userDto = new UserDto();
        //    userDto.UserId = userInfoResponse.Json.TryGetString("sub");
        //    userDto.UserName = loginPo.UserName;
        //    userDto.AccessToken = tokenResponse.AccessToken;
        //    userDto.ExpiresIn = tokenResponse.ExpiresIn;

        //    return userDto;
        //}
    }
}
