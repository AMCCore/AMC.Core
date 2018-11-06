using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AMC.Core.Abstractions.Cache;
using AMC.Core.Abstractions.Logger;
using AMC.Core.Abstractions.Logger.Extensions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Redis;
using Newtonsoft.Json;

namespace AMC.Core.MS.Extention.Redis.Cache
{
    public class Cache : Abstractions.Cache.Repository.ICacheRepositoryAsync
    {
        private const string _redisConnectionStringName = "RedisConnection";

        private static readonly TimeSpan ShortTermCache = TimeSpan.FromMinutes(5);
        private static readonly TimeSpan LongTermCache = TimeSpan.FromMinutes(30);

        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings();

        private readonly ILogger _logger;

        private static readonly IDistributedCache _cahe = new RedisCache(new RedisCacheOptions() {
            Configuration = System.Configuration.ConfigurationManager.ConnectionStrings[_redisConnectionStringName].ConnectionString
        });

        public Cache(ILoggerFactory LoggerFactory)
        {
            _logger = LoggerFactory.Create(typeof(Cache));
        }

        #region RedisCacheNameCompare

        private class RedisCacheNameCompare : IEqualityComparer<string>
        {
            public bool Equals(string x, string y)
            {
                return string.Equals(x, y, StringComparison.OrdinalIgnoreCase);
            }

            public int GetHashCode(string obj)
            {
                return obj.GetHashCode();
            }
        }

        #endregion

        private const string _cache_name_prefix = "__cc_t___";

        private static string MakeCacheKey(string key)
        {
            return string.Concat(_cache_name_prefix, key);
        }

        private static DistributedCacheEntryOptions MakeOptions(CacheState state)
        {
            DistributedCacheEntryOptions _res = new DistributedCacheEntryOptions();

            switch (state)
            {
                case CacheState.NoCache:
                    return null;
                default:
                case CacheState.Default:
                case CacheState.ShortTerm:
                    _res.SlidingExpiration = ShortTermCache;
                    break;
                case CacheState.LongTerm:
                    _res.SlidingExpiration = LongTermCache;
                    break;
                case CacheState.Permanent:
                    _res.AbsoluteExpiration = DateTime.Now.AddHours(12);
                    break;

            }

            return _res;
        }

        public object Load(ICacheable item)
        {
            _logger?.Debug("Cache load started");
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var key = MakeCacheKey(item.CacheKey);

            var value = _cahe.GetString(key);

            if (string.IsNullOrWhiteSpace(value))
                return null;

            object result;
            try
            {
                result = JsonConvert.DeserializeObject(value, SerializerSettings);
            }
            catch (JsonException ex)
            {
                _logger?.Warn(string.Format("Deserializing error key=[{0}] value=[{1}]", key, value), ex);
                throw;
            }

            _logger?.Debug("Cache load finished");
            return item.LoadFromCache(result);
        }

        public async Task<object> LoadAsync(ICacheable item)
        {
            _logger?.Debug("Cache async load started");
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var key = MakeCacheKey(item.CacheKey);

            var value = await _cahe.GetStringAsync(key);

            if (string.IsNullOrWhiteSpace(value))
                return null;

            object result;
            try
            {
                result = JsonConvert.DeserializeObject(value, SerializerSettings);
            }
            catch (JsonException ex)
            {
                _logger?.Warn(string.Format("Deserializing error key=[{0}] value=[{1}]", key, value), ex);
                throw;
            }

            _logger?.Debug("Cache async load finished");
            return item.LoadFromCache(result);
        }

        public void Remove(params ICacheable[] items)
        {
            _logger?.Debug("Cache remove started");

            if (items == null)
                return;

            foreach(var item in items)
            {
                _cahe.Remove(MakeCacheKey(item.CacheKey));
            }

            _logger?.Debug("Cache remove finished");
        }

        public async Task RemoveAsync(params ICacheable[] items)
        {
            _logger?.Debug("Cache async remove started");

            if (items == null)
                return;

            foreach (var item in items)
            {
                await _cahe.RemoveAsync(MakeCacheKey(item.CacheKey));
            }

            _logger?.Debug("Cache async remove finished");
        }

        public void Save(ICacheable item)
        {
            _logger?.Debug("Cache save started");
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var _options = MakeOptions(item.CacheState);
            if (_options == null)
                return;

            var key = MakeCacheKey(item.CacheKey);

            _cahe.SetString(key, JsonConvert.SerializeObject(item.SaveToCache(), SerializerSettings), _options);

            _logger?.Debug("Cache save finished");
        }

        public async Task SaveAsync(ICacheable item)
        {
            _logger?.Debug("Cache async save started");
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            var _options = MakeOptions(item.CacheState);
            if (_options == null)
                return;

            var key = MakeCacheKey(item.CacheKey);

            await _cahe.SetStringAsync(key, JsonConvert.SerializeObject(item.SaveToCache(), SerializerSettings), _options);

            _logger?.Debug("Cache async save finished");
        }
    }
}
