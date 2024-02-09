using Microsoft.Extensions.Caching.Memory;

namespace MydemoFirst.DataAccess.CacheService
{
    public class CacheService : ICacheService
    {
        private static readonly TimeSpan DefaultExp = TimeSpan.FromMinutes(5);

        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<T> GetOrCreate<T>(string key, Func<CancellationToken, Task<T>> factory, TimeSpan? expritation = null, CancellationToken cancellation = default)
        {
            try
            {
                T result = await _memoryCache.GetOrCreateAsync(key, entry =>
                {
                    entry.SlidingExpiration = (expritation ?? DefaultExp);
                    return factory(cancellation);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public T GetData<T>(string key)
        {
            try
            {
                if (_memoryCache.TryGetValue(key, out T result))
                {
                    return result;
                }

                return default(T);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



        public bool SetData<T>(string key, T value, TimeSpan exp)
        {
            bool res = true;
            try
            {
                T item = (T)_memoryCache.Get(key);

                if (!string.IsNullOrEmpty(key))
                {
                    _memoryCache.Set(key, value, exp);
                }
                else
                {
                    res = false;
                }

                return res;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool RemoveData(string key)
        {

            try
            {
                if (_memoryCache.TryGetValue(key, out object result))
                {
                    if (result != null)
                    {
                        _memoryCache.Remove(key);

                        return true;
                    }

                }
                return false;
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }



        }

        public bool TryGetData<T>(string key, out T cacheT)
        {

            if (_memoryCache.TryGetValue(key, out T cachedResponse))
            {
                cacheT = (T)cachedResponse;
                return true;
            }

            cacheT = default(T);

            return false;
        }
    }
}