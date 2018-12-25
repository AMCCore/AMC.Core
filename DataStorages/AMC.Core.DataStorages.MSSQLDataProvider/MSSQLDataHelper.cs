using AMC.Core.Abstractions.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.DataStorages.MSSQLDataProvider
{
    public class MSSQLDataHelper : DataHelper
    {
        private const string _ReturnValueParameterName = "RETURN_VALUE";
        private const string _ConnectionStringName = "main";

        private string ConnectionString
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ConnectionString))
                {
                    _ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[_ConnectionStringName].ConnectionString;
                }
                return _ConnectionString;
            }
        }

        private string _ConnectionString;// "Data Source=37.140.192.244;Initial Catalog=u0283737_DM2;Integrated Security=False;User ID=u0283737_adm;Password=Jrz#512r";

        public override IDataAdapter CreateAdapter(IDbCommand Command)
        {
            return new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)Command);
        }

        public override IDbDataParameter CreateDataParameterWithValue(string Name, ParameterDirection Direction, object Value)
        {
            return new System.Data.SqlClient.SqlParameter(Name, Value) { Direction = Direction };
        }

        public override IDbConnection OpenConnection()
        {
            System.Data.SqlClient.SqlConnection c = new System.Data.SqlClient.SqlConnection(ConnectionString);
            c.Open();
            return c;
        }

        public override string ReturnValueParameterName()
        {
            return _ReturnValueParameterName;
        }
    }
}
