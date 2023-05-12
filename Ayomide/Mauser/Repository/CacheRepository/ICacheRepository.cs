using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.CacheRepository
{
    public interface ICacheRepository<T> where T : class
    {
        Task<T?> Get(string key);
        Task Save(string key, T value);

        Task Delete(string key);
    }
}
