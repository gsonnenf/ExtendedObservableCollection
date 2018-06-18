/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Collections.Observable.Base {
    public abstract class ObservableBaseMap<TKey,TValue> : ObservableNotifyDictionaryBase<TKey,TValue>, IDictionary<TKey,TValue> {


        protected abstract IDictionary<TKey, TValue> InternalDictionary { get; }

        public abstract TValue this[TKey key] { get; set; }
        public abstract void Clear();
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);

        #region Passthrough
        public int Count => InternalDictionary.Count;
        public bool IsReadOnly => InternalDictionary.IsReadOnly;
        public IEnumerator GetEnumerator() => InternalDictionary.GetEnumerator();     
        public bool ContainsKey(TKey key) => InternalDictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => InternalDictionary.TryGetValue(key,  out value);
        #endregion

        #region Icollection
        public ICollection<TKey> Keys => InternalDictionary.Keys;
        public ICollection<TValue> Values => InternalDictionary.Values;

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => InternalDictionary.Contains(item);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => InternalDictionary.CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => InternalDictionary.GetEnumerator();

        #endregion

    }
}
*/