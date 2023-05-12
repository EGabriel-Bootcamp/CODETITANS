using Domain.Entities;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Services.CacheRepository
{
    public class CacheRepository<T> : ICacheRepository<T> where T : class
    {

        private readonly IDistributedCache _distributedCache;
        public CacheRepository(IDistributedCache distributedCache) {
            _distributedCache = distributedCache;   
        }

        public async Task Delete(string key)

        {
            try
            {
                await _distributedCache.RemoveAsync(key);
            }
            catch (Exception e)
            {

              
            }

          
        }

        public async Task<T?> Get(string key)
        {

            try
            {
                var redisData = await _distributedCache.GetAsync(key);
                if (redisData == null)
                    return null;

                var serializedData = Encoding.UTF8.GetString(redisData);
                var options = new JsonSerializerOptions { WriteIndented = true };
                return JsonSerializer.Deserialize<T>(serializedData, options: options);
            }
            catch (Exception)
            {

                return null;
            }

        }

        public async Task Save(string key, T value)
        {
            try
            {
                var options = new JsonSerializerOptions { WriteIndented = true };
                var serializedData = JsonSerializer.Serialize(value, options: options);
                var serializedBytes = Encoding.UTF8.GetBytes(serializedData);


                var cacheOptions = new DistributedCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTime.Now.AddMinutes(10))
                    .SetSlidingExpiration(TimeSpan.FromMinutes(2));

                await _distributedCache.SetAsync(key, serializedBytes, cacheOptions);
            }
            catch (Exception e)
            {

              
            }

        }
    }
}
