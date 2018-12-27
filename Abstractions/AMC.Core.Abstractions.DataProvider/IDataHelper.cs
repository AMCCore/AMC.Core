using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public interface IDataHelper
    {
        bool SupressError { get; }

        string ReturnValueParameterName();

        System.Data.IDbDataParameter CreateDataParameterWithValue(string Name, System.Data.ParameterDirection Direction, object Value);
    }
}
