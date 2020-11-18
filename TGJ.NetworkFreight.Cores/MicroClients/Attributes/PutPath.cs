using System;
using System.Collections.Generic;
using System.Text;

namespace TGJ.NetworkFreight.Cores.MicroClients.Attributes
{
    /// <summary>
    /// Put请求特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PutPath : Attribute
    {
        // 请求路径
        public string Path { get; }
        public PutPath(string Path)
        {
            this.Path = Path;
        }
    }
}
