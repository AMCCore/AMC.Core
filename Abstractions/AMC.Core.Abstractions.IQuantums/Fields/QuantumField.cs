using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums.Fields
{
    public class QuantumField
    {
        public uint Id { get; private set; }

        public string Code { get; private set; }

        public DataType DataType { get; private set; }
    }
}
