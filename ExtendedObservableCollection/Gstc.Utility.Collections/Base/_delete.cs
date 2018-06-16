/*
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gstc.Collections.Observable.Base {
    public abstract partial class ObservableMapListBase<TKey, TValue>  {

        bool IDictionary.IsReadOnly => ((IDictionary)Map).IsReadOnly;

        bool IDictionary.IsFixedSize => ((IDictionary)Map).IsFixedSize;

        object IDictionary.this[object key] {
            get {
                if (!IsCompatibleKey(key)) return null;
                return Map[(TKey)key];
            }
            set {
                if (!IsCompatibleKey(key)) throw new ArgumentException("Key is of incompatible type.");
                if (!IsCompatibleValue(value)) throw new ArgumentException("Value is of incompatible type.");
                this[(TKey)key] = (TValue)value;
            }
        }

        void IDictionary.Add(object keyObj, object valueObj) {
            IsCompatibleKey(keyObj);
            IsCompatibleKey(valueObj);
            Add((TKey)keyObj, (TValue)valueObj);
        }

        void IDictionary.Clear() => Clear();

        bool IDictionary.Contains(object key) => ((IDictionary)Map).Contains(key);

        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)Map).GetEnumerator();

        void IDictionary.Remove(object keyObj) {
            CheckReentrancy(); // Am i stacking these things?
            IsCompatibleKey(keyObj);
            var key = (TKey)keyObj;
            Remove(key);
        }

        ICollection IDictionary.Keys => ((IDictionary)Map).Keys;

        ICollection IDictionary.Values => ((IDictionary)Map).Values;

        private static bool IsCompatibleKey(object key) {
            if (key == null) throw new ArgumentNullException("Key is null");
            return key is TKey;
        }

        private static bool IsCompatibleValue(object value) => value is TValue;
    }
}
*/