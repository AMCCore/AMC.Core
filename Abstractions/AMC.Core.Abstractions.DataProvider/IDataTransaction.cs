using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public interface IDataTransaction : IDataStorage
    {
        SqlDataTransactionState State { get; }

        EventHandler BeforeCommit { get; }

        EventHandler AfterCommit { get; }

        EventHandler BeforeRollback { get; }

        EventHandler AfterRollback { get; }

        void Commit();

        void RollBack();
    }
}
