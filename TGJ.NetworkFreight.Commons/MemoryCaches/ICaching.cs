using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.Commons.MemoryCaches
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICaching
    {
        object Get(string cacheKey);

        void Set(string cacheKey, object cacheValue);

        void Update(string cacheKey, object cacheValue);
    }
}
