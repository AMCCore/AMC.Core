using AMC.Core.Abstractions.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.DataStorages.MSSQLDataProvider
{
    public sealed class MSSQLDataStoage : TransactDataStorage
    {
        public MSSQLDataStoage() : base(new MSSQLDataHelper())
        {
        }

        //protected override SqlCommandProperties PrepareCommand(string query, CommandType type)
        //{
        //    return new SqlCommandProperties(this.Helper.OpenConnection(), query, type, true);
        //}

        public object ExecuteScalar(string query, params System.Data.SqlClient.SqlParameter[] parameters)
        {
            return base.ExecuteScalar(query, parameters);
        }

        public override DataTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            return new MSSQLDataTransaction(level, this.Helper);
        }

        public override void Dispose()
        {
        }
    }
}
