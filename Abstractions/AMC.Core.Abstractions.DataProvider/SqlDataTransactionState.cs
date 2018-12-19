using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public enum SqlDataTransactionState : int
    {
        Open = 1,
        Committed = 2,
        RollBack = 3,
        Error = 4
    }
}
