using AMC.Core.Abstractions.Logger;
using System.Data;

namespace AMC.Core.Abstractions.DataProvider.Helpers
{
    public interface IDataHelper
    {
        bool SupressError { get; }

        string ReturnValueParameterName();

        IDbDataParameter CreateDataParameterWithValue(string Name, ParameterDirection Direction, object Value);

        IDbConnection OpenConnection();

        IDataAdapter GetDefaultAdapter(IDbCommand Command);

        ILogger Logger { get; }
    }
}
