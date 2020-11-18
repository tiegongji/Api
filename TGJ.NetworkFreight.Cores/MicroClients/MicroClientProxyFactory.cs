using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Text;
using TGJ.NetworkFreight.Cores.DynamicMiddleware;

namespace TGJ.NetworkFreight.Cores.MicroClients
{
    /// <summary>
    /// 创建微服务客户端代理
    /// </summary>
    public class MicroClientProxyFactory
    {
        private readonly IDynamicMiddleService middleService;

        public MicroClientProxyFactory(IDynamicMiddleService middleService)
        {
            this.middleService = middleService;
        }

        /// <summary>
        /// 创建接口代理类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object CreateMicroClientProxy(Type type)
        {
            ProxyGenerator proxyGenerator = new ProxyGenerator();
            object t = proxyGenerator.CreateInterfaceProxyWithoutTarget(type, new MicroClientProxy(middleService));
            return t;
        }
    }
}
