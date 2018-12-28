
namespace AMC.Core.Abstractions.DataProvider.Helpers
{
    public interface IDataHelper
    {
        bool SupressError { get; }

        string ReturnValueParameterName();

        System.Data.IDbDataParameter CreateDataParameterWithValue(string Name, System.Data.ParameterDirection Direction, object Value);
    }
}
