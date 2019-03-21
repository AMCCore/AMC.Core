using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumModel.QuantumFields
{
    public struct QuantumFieldEqualityComparer : IEqualityComparer<IQuantumField>
    {
        public bool Equals(IQuantumField x, IQuantumField y)
        {
            if (x == null || y == null)
                return false;

            return x.Id == y.Id && string.Equals(x.Code, y.Code, StringComparison.Ordinal);
        }

        public int GetHashCode(IQuantumField obj)
        {
            return obj.GetHashCode();
        }
    }
}
