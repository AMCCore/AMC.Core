using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumFields
{
    public struct QuantumField
    {
        public uint Id { get; private set; }

        public DataType DataType { get; set; }

        public string Code { get; set; }
    }
}
