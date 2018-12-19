using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AMC.Core.Abstractions.QuantumBasis.QuantumFields;

namespace AMC.Core.Abstractions.QuantumBasis
{
    public class QuantumValueCollection : IEnumerable<KeyValuePair<QuantumField, object>>, IEnumerable
    {
        private readonly Dictionary<QuantumField, object> _dict = new Dictionary<QuantumField, object>(new QuantumFieldComparer());

        public int Count => _dict.Count;

        public IEnumerator GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        IEnumerator<KeyValuePair<QuantumField, object>> IEnumerable<KeyValuePair<QuantumField, object>>.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        public object this[string Code]
        {
            get
            {
                var f = _dict.Keys.First(ss => string.Equals(ss.Code, Code, StringComparison.OrdinalIgnoreCase));
                return _dict[f];
            }
            set
            {
                var f = _dict.Keys.First(ss => string.Equals(ss.Code, Code, StringComparison.OrdinalIgnoreCase));
                _dict[f] = value;
            }
        }

        public object this[uint Id]
        {
            get
            {
                var f = _dict.Keys.First(ss => ss.Id == Id);
                return _dict[f];
            }
            set
            {
                var f = _dict.Keys.First(ss => ss.Id == Id);
                _dict[f] = value;
            }
        }

        public object this[QuantumField Field]
        {
            get
            {
                return _dict[Field];
            }
            set
            {
                _dict[Field] = value;
            }
        }

        private bool TryGetValue(uint key, out object value)
        {
            var f = _dict.Keys.FirstOrDefault(ss => ss.Id == key);
            if (f.Id > 0)
            {
                value = _dict[f];
                return true;
            }
            value = null;
            return false;
        }

        private bool TryGetValue(string key, out object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var f = _dict.Keys.FirstOrDefault(ss => string.Equals(key, ss.Code, StringComparison.OrdinalIgnoreCase));
            if (f.Id > 0)
            {
                value = _dict[f];
                return true;
            }
            value = null;
            return false;
        }

    }
}
