using Application.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Infrastructure.Services
{
    public class CacheService(IDistributedCache _cache) : ICacheService
    {
        
        public async Task DeleteAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
          var serializedData= await _cache.GetStringAsync(key);

            if(string.IsNullOrEmpty(serializedData))
                return default;

            return JsonSerializer.Deserialize<T>(serializedData);
        }

        public async Task SetAsync<T>(string key, T value, TimeSpan? AbsoluteExpiration = null, TimeSpan? slidingExpiration = null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = AbsoluteExpiration ?? TimeSpan.FromMinutes(30),
                SlidingExpiration = slidingExpiration ?? TimeSpan.FromMinutes(5)
            };

            var serializedData=JsonSerializer.Serialize(value);

            await _cache.SetStringAsync(key, serializedData, options);
        }
    }
}
