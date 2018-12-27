using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public interface ITransactDataStorage
    {
        IDataTransaction BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted);
    }
}
