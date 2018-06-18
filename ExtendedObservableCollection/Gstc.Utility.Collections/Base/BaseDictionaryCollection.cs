using System;
using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Interface;
using Gstc.Collections.Observable.Notify;

namespace Gstc.Collections.Observable.Base {
    public abstract class BaseDictionaryCollection<TKey, TValue> : 
        NotifyDictionaryCollectionBase<TKey, TValue>, 
        IObservableDictionaryCollection<TKey,TValue> {

        protected abstract ICollection<TValue> InternalCollection { get; }
            
        protected abstract IDictionary<TKey, TValue> InternalDictionary { get; }
        //Dictionary
        public abstract TValue this[TKey key] { get; set; }
       
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);
        public abstract void Clear();
     

        #region Icollection

        IEnumerator IEnumerable.GetEnumerator() => InternalDictionary.GetEnumerator();
        public int Count => InternalDictionary.Count;
        public bool IsReadOnly => InternalDictionary.IsReadOnly;
        public IEnumerator<TValue> GetEnumerator() => InternalCollection.GetEnumerator();
        public bool Contains(TValue item) => InternalCollection.Contains(item);
        public void CopyTo(TValue[] array, int arrayIndex) => InternalCollection.CopyTo(array, arrayIndex);
       
        void ICollection<TValue>.Add(TValue item) => throw new NotImplementedException();
        bool ICollection<TValue>.Remove(TValue item) =>  throw new NotImplementedException();

        //IDictionary
        public bool ContainsKey(TKey key) => InternalDictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => InternalDictionary.TryGetValue(key, out value);

        public ICollection<TKey> Keys => InternalDictionary.Keys;
        public ICollection<TValue> Values => InternalDictionary.Values;
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => InternalDictionary.Contains(item);
        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => InternalDictionary.CopyTo(array, arrayIndex);
        public bool Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);

        
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
            => InternalDictionary.GetEnumerator();

        #endregion

       
    }
}
