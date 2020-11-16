using System;
using System.Collections.Generic;
using System.Text;
using TGJ.NetworkFreight.Cores.Cluster.Options;
using TGJ.NetworkFreight.Cores.Middleware.Options;
using TGJ.NetworkFreight.Cores.Registry.Options;

namespace TGJ.NetworkFreight.Cores.DynamicMiddleware.Options
{
    /// <summary>
    /// 中台配置选项
    /// </summary>
    public class DynamicMiddlewareOptions
    {
        public DynamicMiddlewareOptions()
        {
            serviceDiscoveryOptions = options => { };
            loadBalanceOptions = options => { };
            middlewareOptions = options => { };
        }

        /// <summary>
        /// 服务发现选项
        /// </summary>
        public Action<ServiceDiscoveryOptions> serviceDiscoveryOptions { set; get; }

        /// <summary>
        /// 负载均衡选项
        /// </summary>
        public Action<LoadBalanceOptions> loadBalanceOptions { set; get; }

        // 中台选项
        public Action<MiddlewareOptions> middlewareOptions { set; get; }
    }
}
