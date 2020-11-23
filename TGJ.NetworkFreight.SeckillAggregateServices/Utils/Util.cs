using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.SeckillAggregateServices.Utils
{
    /// <summary>
    /// 工具类
    /// </summary>
    public class Util
    {
        /// <summary>
        /// 获取短信Code
        /// </summary>
        /// <returns></returns>
        public static string GetSmsCode()
        {
            Random random = new Random();
            return random.Next(1000, 9999).ToString();
        }
    }
}
