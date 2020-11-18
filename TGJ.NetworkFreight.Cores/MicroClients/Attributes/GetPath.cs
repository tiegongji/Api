using System;
using System.Collections.Generic;
using System.Text;

namespace TGJ.NetworkFreight.Cores.MicroClients.Attributes
{
    /// <summary>
    /// Get方法请求
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class GetPath : Attribute
    {
        // 请求路径
        public string Path { get; } // path/
        public GetPath(string Path)
        {
            this.Path = Path;
        }
    }
}
