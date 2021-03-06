﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TGJ.NetworkFreight.Commons.MemoryCaches
{
    /// <summary>
    /// 实例化缓存接口ICaching
    /// </summary>
    public class MemoryCaching : ICaching
    {
        private readonly IMemoryCache _cache;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="cache"></param>
        public MemoryCaching(IMemoryCache cache)
        {
            _cache = cache;
        }

        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }

        public void Update(string cacheKey, object cacheValue)
        {
            _cache.Remove(cacheKey);

            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(3600));
        }

        public void Set(string cacheKey, object cacheValue)
        {
            _cache.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(3600));
        }
    }
}
