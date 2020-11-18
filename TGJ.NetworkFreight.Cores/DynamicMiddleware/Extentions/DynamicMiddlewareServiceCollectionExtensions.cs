﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TGJ.NetworkFreight.Cores.Cluster.Extentions;
using TGJ.NetworkFreight.Cores.DynamicMiddleware.Options;
using TGJ.NetworkFreight.Cores.DynamicMiddleware.Urls;
using TGJ.NetworkFreight.Cores.Middleware.Extentions;
using TGJ.NetworkFreight.Cores.Registry.Extentions;

namespace TGJ.NetworkFreight.Cores.DynamicMiddleware.Extentions
{
    /// <summary>
    ///  中台ServiceCollection扩展方法
    /// </summary>
    public static class DynamicMiddlewareServiceCollectionExtensions
    {
        /// <summary>
        /// 添加动态中台
        /// </summary>
        /// <typeparam name="IMiddleService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services)
            where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(services, options => { });
            return services;
        }

        /// <summary>
        /// 添加动态中台
        /// </summary>
        /// <typeparam name="IMiddleService"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDynamicMiddleware<TMiddleService, TMiddleImplementation>(this IServiceCollection services, Action<DynamicMiddlewareOptions> options)
            where TMiddleService : class
            where TMiddleImplementation : class, TMiddleService
        {
            DynamicMiddlewareOptions dynamicMiddlewareOptions = new DynamicMiddlewareOptions();
            options(dynamicMiddlewareOptions);

            // 1、注册服务发现
            services.AddServiceDiscovery(dynamicMiddlewareOptions.serviceDiscoveryOptions);

            // 2、注册负载均衡
            services.AddLoadBalance(dynamicMiddlewareOptions.loadBalanceOptions);

            // 3、添加中台服务
            services.AddMiddleware(dynamicMiddlewareOptions.middlewareOptions);

            // 4、注册动态中台Url服务
            services.AddSingleton<IDynamicMiddleUrl, DefaultDynamicMiddleUrl>();

            // 5、注册动态服务
            services.AddSingleton<TMiddleService, TMiddleImplementation>();
            return services;
        }
    }
}
