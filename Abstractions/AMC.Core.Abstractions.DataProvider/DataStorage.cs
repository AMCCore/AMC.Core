using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.DataProvider
{
    public abstract class DataStorage : IDisposable
    {
        protected readonly DataHelper Helper;

        public event EventHandler<EventArgs.ErrorEventArgs> Error;

        protected virtual void OnError(EventArgs.ErrorEventArgs args)
        {
            Error?.Invoke(this, args);
        }

        protected DataStorage(DataHelper Helper)
        {
            this.Helper = Helper;
        }

        public abstract void Dispose();
    }
}
