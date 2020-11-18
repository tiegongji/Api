using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TGJ.NetworkFreight.Commons.Exceptions;

namespace TGJ.NetworkFreight.Commons.Users
{
    /// <summary>
    /// 系统用户模型绑定
    /// 1、将HttpContext用户信息转换成为Sysuser
    /// 2、将Sysuser绑定到action方法参数
    /// </summary>
    public class SysUserModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            if (bindingContext == null)
            {
                throw new ArgumentNullException(nameof(bindingContext));
            }
            if (bindingContext.ModelType == typeof(SysUser))
            {
                // 1、转换到指定模型
                //  SysUser sysUser = (SysUser)bindingContext.Model;
                SysUser sysUser = new SysUser();

                // 2、设置模型值
                HttpContext httpContext = bindingContext.HttpContext;
                ClaimsPrincipal claimsPrincipal = httpContext.User;

                IEnumerable<Claim> claims = claimsPrincipal.Claims;
                // 1、判断申明是否为空，如果为空，没有登录，抛出异常
                if (claims.ToList().Count == 0)
                {
                    throw new BizException("授权失败，没有登录");
                }
                foreach (var claim in claims)
                {
                    // 1、获取用户Id
                    if (claim.Type.Equals("sub"))
                    {
                        sysUser.UserId = Convert.ToInt32(claim.Value);
                    }

                    // 2、获取用户名
                    if (claim.Type.Equals("amr"))
                    {
                        sysUser.UserName = claim.Value;
                    }
                }

                // 3、返回结果
                bindingContext.Result = ModelBindingResult.Success(sysUser);

            }

            return Task.CompletedTask;
        }
    }
}
