using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumAdapter
{
    public interface IPopulator<T> where T : QuantumBasis.BaseQuantum
    {
        DataProvider.QueryBuilder.IQueryBuilder CreateOrUpdate(T entiity);

        T Populate(object entiity);

        DataProvider.QueryBuilder.IQueryBuilder BaseLoad();
    }
}
