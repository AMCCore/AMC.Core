using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumFields
{
    internal class QuantumFieldComparer : IEqualityComparer<QuantumField>
    {
        public bool Equals(QuantumField x, QuantumField y)
        {
            return x.Id == y.Id && string.Equals(x.Code, y.Code, StringComparison.OrdinalIgnoreCase);
        }

        public int GetHashCode(QuantumField obj)
        {
            return obj.Id.GetHashCode() + obj.Code.GetHashCode();
        }
    }
}
