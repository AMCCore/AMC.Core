using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public interface IDataStorage : IDisposable
    {
        IDataHelper Helper { get; }

        event EventHandler<EventArgs.ErrorEventArgs> Error;
    }
}
