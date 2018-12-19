using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumTypes
{
    public class QuantumUserType : BaseQuantumType
    {
        private const string _code = "User";

        public QuantumUserType() : base(_code)
        {
        }

        protected override uint GetSpecialId()
        {
            return 1;
        }
    }
}
