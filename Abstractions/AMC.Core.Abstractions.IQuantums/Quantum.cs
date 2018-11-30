using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace AMC.Core.Abstractions.Quantums
{
    public class Quantum : AQuantum
    {
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            if (string.Equals(binder.Name, nameof(Creator), StringComparison.OrdinalIgnoreCase))
            {
                result = Creator;
                return Creator != null;
            }
            if (string.Equals(binder.Name, nameof(Updater), StringComparison.OrdinalIgnoreCase))
            {
                result = Updater;
                return Updater != null;
            }

            return base.TryGetMember(binder, out result);
        }

        public User Creator { get; private set; }

        public User Updater { get; private set; }

    }
}
