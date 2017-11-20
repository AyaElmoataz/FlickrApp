using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApp.Services.ServicesHelpers
{
    public class MemoryCacheService : ICacheService
    {
        private static readonly Dictionary<string, object> CachedData = new Dictionary<string, object>();
        public T GetData<T>(string cacheKey)
        {
            lock (CachedData)
            {
                if (CachedData.ContainsKey(cacheKey))
                {
                    return (T)CachedData[cacheKey];
                }
                else
                    return default(T);
            }
        }

        public void SaveData<T>(string cacheKey, T content)
        {
            lock (CachedData)
                CachedData[cacheKey] = content;
        }
    }
}
