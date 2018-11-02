using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.Logger.Extensions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AMC.Core.MS.Extention.Cache
{
    public class Cache : Abstractions.Cache.Repository.IFlushableCacheRepository
    {
        private static readonly TimeSpan ShortTermCache = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan LongTermCache = TimeSpan.FromMinutes(30);

        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private static readonly IMemoryCache _cache = new MemoryCache(new MemoryCacheOptions());

        private readonly ILogger _logger;

        public Cache(ILoggerFactory LoggerFactory)
        {
            _logger = LoggerFactory.Create(typeof(Cache));
        }

        public object Load(ICacheable item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            object data = Get(item.CacheKey);
            if (data != null)
                return item.LoadFromCache(data);
            else return null;
        }

        public void Remove(params ICacheable[] items)
        {
            Parallel.ForEach(items, (item) =>
            {
                Remove(item.CacheKey);
            });
        }

        public void Save(ICacheable item)
        {
            _logger?.Log("Cache save started");
            if (item == null)
                throw new ArgumentNullException(nameof(item));
            Set(item);
        }

        private void Set(ICacheable item)
        {
            if (_cache == null) return;

            MemoryCacheEntryOptions _options = new MemoryCacheEntryOptions()
            {
                Priority = CacheItemPriority.Normal,
            };
            _options.AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token));

            switch (item.CacheState)
            {
                case CacheState.NoCache:
                    return;
                case CacheState.ShortTerm:
                    _options.SlidingExpiration = ShortTermCache;
                    break;
                case CacheState.LongTerm:
                    _options.SlidingExpiration = LongTermCache;
                    break;
                case CacheState.Permanent:
                    _options.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    break;
                default:
                    throw new NotSupportedException();
            }

            string newKey = MakeCacheKey(item.CacheKey);

            _cache.Set(newKey, item, _options);
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

        private const string _cache_name_prefix = "__cc_t___";

        private static string MakeCacheKey(string key)
        {
            return string.Concat(_cache_name_prefix, key);
        }

        public void FlushCache()
        {
            if (_resetCacheToken != null && !_resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }
    }
}
