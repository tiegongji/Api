using System;
using System.Collections.Generic;
using System.Text;
using TGJ.NetworkFreight.Cores.DynamicMiddleware.Options;

namespace TGJ.NetworkFreight.Cores.MicroClients.Options
{
    /// <summary>
    /// 客户端代理选项
    /// </summary>
    public class MicroClientOptions
    {
        public MicroClientOptions()
        {
            dynamicMiddlewareOptions = options => { };
        }

        /// <summary>
        /// 程序集名称
        /// </summary>
        public string AssmelyName { set; get; }

        /// <summary>
        /// 动态中台选项
        /// </summary>
        public Action<DynamicMiddlewareOptions> dynamicMiddlewareOptions { set; get; }

    }
}
