using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumModel
{
    /// <summary>
    /// 
    /// </summary>
    public interface IQuant : Cache.ICacheable
    {
        ulong Id { get; }

        uint TypeId { get; }

        QuantumTypes.IQuantumType Type { get; }

        IQuantumValueCollection Values { get; }
    }
}
