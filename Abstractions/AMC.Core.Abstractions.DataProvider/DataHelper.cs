
using System.Data;

namespace AMC.Core.Abstractions.DataProvider
{
    public abstract class DataHelper
    {
        public abstract string ReturnValueParameterName();

        public abstract IDbDataParameter CreateDataParameterWithValue(string Name, ParameterDirection Direction, object Value);

        public abstract IDbConnection OpenConnection();

        public bool SupressError { get; }

        public DataHelper(bool SupressError = false)
        {
            this.SupressError = SupressError;
        }

        public abstract IDataAdapter CreateAdapter(IDbCommand Command);
    }
}
