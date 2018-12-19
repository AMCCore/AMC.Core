using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumFields
{
    public enum DataType : ushort
    {
        String = 1,
        Text = 2,
        Date = 3,
        DateTime = 4,
        Bool = 5,
        Integer = 6,
        Decimal = 6,
        Link = 8,

        TimeSpan = 9
    }
}
