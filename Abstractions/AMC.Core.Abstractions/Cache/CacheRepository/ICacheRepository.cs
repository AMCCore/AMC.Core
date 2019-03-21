using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMC.Core.Abstractions.Cache.Repository
{
    public interface ICacheRepository
    {
        void Save(ICacheable item);

        object Load(ICacheable item);

        void Remove(params ICacheable[] items);
    }
}
