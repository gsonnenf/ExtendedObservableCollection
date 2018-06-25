﻿using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Interface;
using Gstc.Collections.Observable.Notify;

namespace Gstc.Collections.Observable.Base {
    public abstract class BaseObservableDictionary<TKey, TValue> : 
        NotifyDictionary<TKey, TValue>,
        IObservableDictionary<TKey, TValue> {

        //Absstract Methods
        protected abstract IDictionary<TKey, TValue> InternalDictionary { get; }
        public abstract TValue this[TKey key] { get; set; }
        public abstract void Clear();
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);
        
        //Dictionary
        public int Count => InternalDictionary.Count;
                public bool ContainsKey(TKey key) => InternalDictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => InternalDictionary.TryGetValue(key, out value);
     
        // Key Value Operations
        public ICollection<TKey> Keys => InternalDictionary.Keys;
        public ICollection<TValue> Values => InternalDictionary.Values;
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => InternalDictionary.Contains(item);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => InternalDictionary.CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => InternalDictionary.IsReadOnly;

        //Enumerators
        public IEnumerator GetEnumerator() => InternalDictionary.GetEnumerator();
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => InternalDictionary.GetEnumerator();
    }
}
