using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums.Fields
{
    public enum DataType : uint
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
