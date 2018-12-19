using System;
using System.Collections.Generic;
using System.Text;
using AMC.Core.Abstractions.QuantumBasis.Populators;
using AMC.Core.Abstractions.QuantumBasis.QuantumTypes;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumUsers
{
    public class QuantumUser : BaseQuantum
    {
        public QuantumUser() : base(new QuantumTypes.QuantumUserType())
        {
        }

        protected override BaseQuantumPopulator<QuantumUser> GetPopulator<T>()
        {
            return new QuantumUserPopulator();
        }
    }
}
