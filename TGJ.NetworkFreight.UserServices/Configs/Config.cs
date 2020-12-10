﻿using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using static IdentityServer4.IdentityServerConstants;

namespace TGJ.NetworkFreight.UserServices.Configs
{
    public class Config
    {
        /// <summary>
        /// 1、微服务API资源
        /// </summary>
        /// <returns></returns>
        //public static IEnumerable<ApiResource> GetApiResources()
        //{
        //    return new List<ApiResource>
        //    {
        //        new ApiResource("TGJService", "TGJService api需要被保护",new List<string> {"role","admin" })
        //    };
        //}


        public static IEnumerable<ApiResource> GetResource()
        {
            return new List<ApiResource>
            {

                new ApiResource("TGJService","my api")
                {
                    Scopes ={"TGJService"},//重要,不配置返回 invalid_scope
                }

            };
        }

        /// <summary>
        /// 2、客户端
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client1",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "TGJService",
                                       IdentityServerConstants.StandardScopes.OpenId, //必须要添加，否则报forbidden错误
                                       IdentityServerConstants.StandardScopes.Profile
                    },

                },
                new Client
                {
                    ClientId = "client-password",
	                // 使用用户名密码交互式验证
	                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AccessTokenLifetime = 3600,
                    AllowOfflineAccess = true,
	                // 用于认证的密码
	                ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

	                // 客户端有权访问的范围（Scopes）
	                AllowedScopes = {"TGJService",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile}
                },
                // openid客户端
                new Client
                {
                    ClientId="client-code",
                    ClientSecrets={new Secret("secret".Sha256())},
                    AllowedGrantTypes=GrantTypes.Code,
                    RequireConsent=false,
                    RequirePkce=true,

                    RedirectUris={ "https://localhost:5006/signin-oidc"}, // 1、客户端地址

                    PostLogoutRedirectUris={ "https://localhost:5006/signout-callback-oidc"},// 2、登录退出地址

                    AllowedScopes=new List<string>{
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "TGJService", // 启用服务授权支持
                    StandardScopes.OfflineAccess,
                    },

                    // 增加授权访问
                    AllowOfflineAccess=true
                }
            };
        }

        /// <summary>
        /// 2.1 openid身份资源
        /// </summary>
        public static IEnumerable<IdentityResource> Ids => new List<IdentityResource>
        {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
        };
        public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("TGJService")
        };


        /// <summary>
        /// 3、测试用户
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>()
            {
                new TestUser
                {
                    SubjectId="1",
                    Username="admin",
                    Password="123456"
                }
            };
        }
    }
}
