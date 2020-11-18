﻿using System;
using System.Collections.Generic;
using System.Text;

namespace TGJ.NetworkFreight.Cores.Registry
{
    /// <summary>
    /// 服务注册
    /// </summary>
    public interface IServiceRegistry
    {
        /// <summary>
        /// 注册服务
        /// </summary>
        void Register();

        /// <summary>
        /// 撤销服务
        /// </summary>
        void Deregister();
    }
}
