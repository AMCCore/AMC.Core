using System;
using System.Runtime.Caching;
using System.Threading.Tasks;

using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.Logger.Extensions;

namespace AMC.Core.BaseCache
{
    public sealed class Cache : Abstractions.Cache.Repository.IFlushableCacheRepository
    {
        private static readonly TimeSpan ShortTermCache = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan LongTermCache = TimeSpan.FromMinutes(30);

        private static readonly MemoryCache _cache = MemoryCache.Default;

        private readonly ILogger _logger;

        public Cache(ILoggerFactory LoggerFactory)
        {
            _logger = LoggerFactory.Create(typeof(Cache));
        }

        #region Save

        public void Save(ICacheable item)
        {
            _logger?.Log("Cache save started");
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Set(item);
        }

        #endregion

        #region Load

        public object Load(ICacheable item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            object data = Get(item.CacheKey);
            if (data != null)
                return item.LoadFromCache(data);
            else return null;
        }

        #endregion

        #region Remove

        public void Remove(params ICacheable[] items)
        {
            Parallel.ForEach(items, (item) =>
            {
                Remove(item.CacheKey);
            });
        }

        #endregion

        private void Set(ICacheable item)
        {
            if (_cache == null) return;

            var itemPolicy = new CacheItemPolicy()
            {
                Priority = CacheItemPriority.Default,
            };

            switch (item.CacheState)
            {
                case CacheState.NoCache:
                    return;
                case CacheState.ShortTerm:
                    itemPolicy.SlidingExpiration = ShortTermCache;
                    break;
                case CacheState.LongTerm:
                    itemPolicy.SlidingExpiration = LongTermCache;
                    break;
                case CacheState.Permanent:
                    itemPolicy.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    break;
                default:
                    throw new NotSupportedException();
            }

            string newKey = MakeCacheKey(item.CacheKey);

            _cache.Add(newKey, item, itemPolicy);
        }

        private object Get(string key)
        {
            if (_cache == null) return null;
            key = MakeCacheKey(key);
            return _cache.Get(key);
        }

        private void Remove(string key)
        {
            if (_cache == null) return;
            _cache.Remove(MakeCacheKey(key));
        }

        private static string MakeCacheKey(string key)
        {
            return string.Concat("__cc_t___", key);
        }

        public void FlushCache()
        {
            lock (_cache)
            {
                foreach(var item in _cache)
                {
                    _cache.Remove(item.Key);
                }
            }
        }
    }
}
