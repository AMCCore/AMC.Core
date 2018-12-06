using System;
using System.Collections.Generic;
using System.Text;

namespace AMC.Core.Abstractions.QuantumBasis.QuantumTypes
{
    public abstract class BaseQuantumType
    {
        protected BaseQuantumType()
        {
        }

        public BaseQuantumType(string Code) : this()
        {
            _baseCode = Code;
        }

        protected readonly string _baseCode;

        public uint Id { get; private set; }

        public string Code
        {
            get
            {
                return _baseCode;
            }
        }

        public uint SpecialId => GetSpecialId();

        protected bool IsSpecial => SpecialId > 0;

        protected virtual uint GetSpecialId()
        {
            return 0;
        }

    }
}
