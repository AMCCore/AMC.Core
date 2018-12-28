using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums.QuantumFields
{
    public interface IQuantumField
    {
        uint Id { get; }

        string Code { get; }

        QuantumFieldDataTypes DataType { get; }
    }
}
