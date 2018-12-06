using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumTypes
{
    public class QuantumUser : BaseQuantumType
    {
        private const string _code = "User";

        public QuantumUser() : base(_code)
        {
        }

        protected override uint GetSpecialId()
        {
            return 1;
        }
    }
}
