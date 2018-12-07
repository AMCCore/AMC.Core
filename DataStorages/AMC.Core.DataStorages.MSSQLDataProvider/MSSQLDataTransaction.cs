using AMC.Core.Abstractions.DataProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.DataStorages.MSSQLDataProvider
{
    public sealed class MSSQLDataTransaction : DataTransaction
    {
        internal MSSQLDataTransaction(IsolationLevel Level, DataHelper Helper) : base(Level, Helper)
        {
        }

        protected override SqlCommandProperties PrepareCommand(string query, CommandType type)
        {
            var res = new SqlCommandProperties(this._connection, query, type, false);
            res.Command.Transaction = this._tran;
            return res;
        }
    }
}
