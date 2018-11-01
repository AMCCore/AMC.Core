using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Core.Abstractions.Cache.Repository
{
    public interface ICacheRepositoryAsync : ICacheRepository
    {
        Task SaveAsync(ICacheable item);

        Task<object> LoadAsync(ICacheable item);

        Task RemoveAsync(params ICacheable[] items);
    }
}
