using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums
{
    public interface IQuantumValueCollection : IEnumerable
    {
        bool ContainsKey(string Key);

        bool ContainsKey(QuantumFields.IQuantumField Key);

        int Count { get; }

        object this[int Index] { get; set; }

        object this[string Key] { get; set; }

        object this[QuantumFields.IQuantumField Key] { get; set; }

        int IndexOf(string Key);

        int IndexOf(QuantumFields.IQuantumField Key);
    }
}
