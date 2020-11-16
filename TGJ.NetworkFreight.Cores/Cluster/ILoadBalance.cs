using System;
using System.Collections.Generic;
using System.Text;
using TGJ.NetworkFreight.Cores.Registry;

namespace TGJ.NetworkFreight.Cores.Cluster
{
    /// <summary>
    /// 服务负载均衡
    /// </summary>
    public interface ILoadBalance
    {
        /// <summary>
        /// 服务选择
        /// </summary>
        /// <param name="serviceUrls"></param>
        /// <returns></returns>
        ServiceNode Select(IList<ServiceNode> serviceUrls);
    }
}
