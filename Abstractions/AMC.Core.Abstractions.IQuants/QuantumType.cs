using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.IQuants
{
    public class QuantumType
    {
        public uint Id { get; private set; }

        public uint? SpecialId { get; private set; }

        public string Code { get; set; }

        public bool IsSpecial => SpecialId.HasValue && SpecialId.Value > 0;



    }
}
