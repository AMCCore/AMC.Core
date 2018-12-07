using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public class SqlCommandProperties : IDisposable
    {
        public readonly IDbCommand Command;
        public readonly IDbConnection Connection;
        public bool CloseConnection;

        public SqlCommandProperties(IDbConnection conn, string query, CommandType type, bool closeConnection)
        {
            Connection = conn;
            if (conn.State == ConnectionState.Closed)
                conn.Open();
            Command = conn.CreateCommand();
            Command.CommandText = query;
            Command.CommandType = type;
            CloseConnection = closeConnection;
        }

        public System.Data.IDataParameterCollection Parameters
        {
            get
            {
                return Command.Parameters;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Command.Dispose();
            if (CloseConnection)
                Connection.Dispose();
        }

        #endregion
    }
}
