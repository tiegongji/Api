using System;
using Consul;
using TGJ.NetworkFreight.Cores.Registry.Options;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Net;
using TGJ.NetworkFreight.Cores.Exceptions;

namespace TGJ.NetworkFreight.Cores.Registry
{
    /// <summary>
    /// consul服务发现实现
    /// </summary>
    public class ConsulServiceDiscovery : AbstractServiceDiscovery
    {
        public ConsulServiceDiscovery(IOptions<ServiceDiscoveryOptions> options) : base(options)
        {
        }

        protected override CatalogService[] RemoteDiscovery(string serviceName)
        {
            // 1、创建consul客户端连接 2s 1、使用单例全局共享 2、使用数据缓存(进程：字典，集合) 3、使用连接池
            var consulClient = new ConsulClient(configuration =>
            {
                //1.1 建立客户端和服务端连接
                configuration.Address = new Uri(serviceDiscoveryOptions.DiscoveryAddress);
            });

            // 2、consul查询服务,根据具体的服务名称查询
            var queryResult = consulClient.Catalog.Service(serviceName).Result;
            // 3、判断请求是否失败
            if (!queryResult.StatusCode.Equals(HttpStatusCode.OK))
            {
                throw new FrameException($"consul连接失败:{queryResult.StatusCode}");
            }

            return queryResult.Response;
        }
    }
}
