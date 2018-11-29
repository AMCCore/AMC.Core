using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums
{
    [Serializable]
    public class QuantumType
    {
        public uint Id { get; private set; }

        public uint? SpecialId { get; private set; }

        public string Code { get; private set; }

        public string Name { get; set; }

        public ICollection<Fields.QuantumField> Fields { get; private set; }
    }
}
