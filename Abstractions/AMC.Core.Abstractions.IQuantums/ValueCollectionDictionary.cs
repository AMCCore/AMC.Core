using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace AMC.Core.Abstractions.Quantums
{
    [Serializable]
    internal sealed class ValueCollectionDictionary : IEnumerable<KeyValuePair<Fields.QuantumField, object>>, IEnumerable
    {
        private Dictionary<Fields.QuantumField, object> _dict = new Dictionary<Fields.QuantumField, object>(new QuantumFieldEqualityComparer());

        private struct QuantumFieldEqualityComparer : IEqualityComparer<Fields.QuantumField>
        {
            public bool Equals(Fields.QuantumField x, Fields.QuantumField y)
            {
                if (x == null || y == null)
                    return false;

                return x.Id == y.Id;
            }

            public int GetHashCode(Fields.QuantumField obj)
            {
                return obj.Id.GetHashCode();
            }
        }

        //public object this[string Code]
        //{
        //    get
        //    {
        //        var f = _dict.Keys.First(ss => string.Equals(ss.Code, Code, StringComparison.OrdinalIgnoreCase));
        //        return _dict[f];
        //    }
        //    set
        //    {
        //        var f = _dict.Keys.First(ss => string.Equals(ss.Code, Code, StringComparison.OrdinalIgnoreCase));
        //        _dict[f] = value;
        //    }
        //}

        //public object this[uint Id]
        //{
        //    get
        //    {
        //        var f = _dict.Keys.First(ss => ss.Id == Id);
        //        return _dict[f];
        //    }
        //    set
        //    {
        //        var f = _dict.Keys.First(ss => ss.Id == Id);
        //        _dict[f] = value;
        //    }
        //}

        public int Count => _dict.Count();

        public IEnumerator GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        IEnumerator<KeyValuePair<Fields.QuantumField, object>> IEnumerable<KeyValuePair<Fields.QuantumField, object>>.GetEnumerator()
        {
            return _dict.GetEnumerator();
        }

        public bool TryGetValue(uint key, out object value)
        {
            var f = _dict.Keys.FirstOrDefault(ss => ss.Id == key);
            if(f != null)
            {
                value = _dict[f];
                return true;
            }
            value = null;
            return false;
        }

        public bool TryGetValue(string key, out object value)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            var f = _dict.Keys.FirstOrDefault(ss => string.Equals(key, ss.Code, StringComparison.OrdinalIgnoreCase));
            if (f != null)
            {
                value = _dict[f];
                return true;
            }
            value = null;
            return false;
        }


    }
}
