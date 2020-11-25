using IdentityModel;
using IdentityServer4.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Exceptions;
using TGJ.NetworkFreight.UserServices.Models;
using TGJ.NetworkFreight.UserServices.Services;

namespace TGJ.NetworkFreight.UserServices.IdentityServer
{
    /// <summary>
    /// 自定义资源持有者验证
    /// (从数据库获取用户信息进行验证)
    /// </summary>
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        // 用户服务
        public readonly IUserService userService;

        public ResourceOwnerPasswordValidator(IUserService userService)
        {
            this.userService = userService;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // 1、根据用户名获取用户
            //User user = userService.GetUser(context.UserName);

            User user = userService.GetUserById(Convert.ToInt32(context.UserName));

            // 2、判断User
            if (user == null)
            {
                throw new BizException($"数据库用户不存在:{context.UserName}");
            }

            if (!context.Password.Equals(user.Phone))
            {
                throw new BizException($"数据库手机号不正确");
            }

            context.Result = new GrantValidationResult(
                        subject: user.Id.ToString(),
                        authenticationMethod: user.Name,
                        claims: GetUserClaims(user));
            await Task.CompletedTask;
        }

        // 用户身份声明
        public Claim[] GetUserClaims(User user)
        {
            return new Claim[]
            {
                new Claim(JwtClaimTypes.Subject, user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Id, user.Id.ToString() ?? ""),
                new Claim(JwtClaimTypes.Name, user.Name?? ""),
                new Claim(JwtClaimTypes.PhoneNumber, user.Phone  ?? ""),
                new Claim(JwtClaimTypes.Role, user.RoleName)
            };
        }
    }
}
