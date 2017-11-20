using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlickrApp.Services.ServicesHelpers
{
    public interface ICacheService
    {
        T GetData<T>(string cacheKey);
        void SaveData<T>(string cacheKey, T content);
    }
}
