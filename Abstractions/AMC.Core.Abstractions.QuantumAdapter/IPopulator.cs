using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IPopulator<T> where T : Quantums.IQuant
    {
        DataProvider.QueryBuilder.IQueryBuilder CreateOrUpdate(T entiity);

        T Populate(object entiity);

        DataProvider.QueryBuilder.IQueryBuilder BaseLoad(long Id);

        DataProvider.QueryBuilder.IQueryBuilder Delete(T entiity);

        T GetCacheble(long Id);
    }
}
