using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.Logger.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;

namespace AMC.Core.MS.Extention.Redis.Cache
{
    public class Cache : Abstractions.Cache.Repository.IFlushableCacheRepositoryAsync
    {
        private static readonly TimeSpan ShortTermCache = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan LongTermCache = TimeSpan.FromMinutes(30);

        private readonly ILogger _logger;

        private readonly IDistributedCache _cahe = new RedisCache(new RedisCacheOptions() {
            Configuration = System.Configuration.ConfigurationManager.ConnectionStrings["RedisConnection"].ConnectionString
        });

        public Cache(ILoggerFactory LoggerFactory)
        {
            _logger = LoggerFactory.Create(typeof(Cache));
        }

        public void FlushCache()
        {
            throw new NotImplementedException();
        }

        public Task FlushCacheAsync()
        {
            throw new NotImplementedException();
        }

        public object Load(ICacheable item)
        {
            throw new NotImplementedException();
        }

        public Task<object> LoadAsync(ICacheable item)
        {
            throw new NotImplementedException();
        }

        public void Remove(params ICacheable[] items)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(params ICacheable[] items)
        {
            throw new NotImplementedException();
        }

        public void Save(ICacheable item)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(ICacheable item)
        {
            throw new NotImplementedException();
        }
    }
}
