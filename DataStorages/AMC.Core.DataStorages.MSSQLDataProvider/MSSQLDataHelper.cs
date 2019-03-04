using System.Data;

using AMC.Core.Abstractions.DataProvider.Helpers;
using AMC.Core.Abstractions.Logger;

namespace AMC.Core.DataStorages.MSSQLDataProvider
{
    public class MSSQLDataHelper : IDataHelper
    {
        private const string _ReturnValueParameterName = "RETURN_VALUE";
        private const string _ConnectionStringName = "main";

        public MSSQLDataHelper(ILoggerFactory LoggerFactory, bool SupressError = false)
        {
            Logger = LoggerFactory.Create(typeof(MSSQLDataHelper));
            this.SupressError = SupressError;
        }

        private string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_connectionString))
                {
                    _connectionString = System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionStringName].ConnectionString;
                }
                return _connectionString;
            }
        }

        public bool SupressError { get; }

        public ILogger Logger { get; }

        private string _connectionString;// "Data Source=37.140.192.244;Initial Catalog=u0283737_DM2;Integrated Security=False;User ID=u0283737_adm;Password=Jrz#512r";

        public IDataAdapter CreateAdapter(IDbCommand Command)
        {
            return new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)Command);
        }

        public IDbDataParameter CreateDataParameterWithValue(string Name, ParameterDirection Direction, object Value)
        {
            return new System.Data.SqlClient.SqlParameter(Name, Value) { Direction = Direction };
        }

        public IDbConnection OpenConnection()
        {
            System.Data.SqlClient.SqlConnection c = new System.Data.SqlClient.SqlConnection(ConnectionString);
            c.Open();
            return c;
        }

        public string ReturnValueParameterName()
        {
            return _ReturnValueParameterName;
        }

        public IDataAdapter GetDefaultAdapter(IDbCommand Command)
        {
            return new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)Command);
        }
    }
}
