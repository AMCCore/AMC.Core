using AMC.Core.Abstractions.QuantumBasis;
using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.QuantumDataProvider
{
    public abstract class QuantumStorage : IDisposable
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }

    public sealed class QuantumStorage<T> : QuantumStorage where T : BaseQuantum
    {


    }
}
