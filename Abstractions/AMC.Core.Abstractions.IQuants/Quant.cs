using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.IQuants
{
    public class Quant
    {
        public ulong Id { get; private set; }

        protected uint TypeId { get; set; }
    }
}
