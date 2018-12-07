using System;
using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable;

namespace Gstc.Collections.Observable.Base {
    public abstract class BaseObservableDictionaryCollection<TKey, TValue> : 
        NotifyDictionaryCollection<TKey, TValue>, 
        IObservableDictionaryCollection<TKey,TValue> {

       //internal collections
        protected abstract ICollection<TValue> InternalCollection { get; }            
        protected abstract IDictionary<TKey, TValue> InternalDictionary { get; }

        //Abstract methods
        public abstract TValue this[TKey key] { get; set; }     
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);
        public abstract void Clear();

        public int Count => InternalDictionary.Count;


        //IDictionary
        public bool ContainsKey(TKey key) => InternalDictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => InternalDictionary.TryGetValue(key, out value);

        //Dictionary
        object IDictionary.this[object key] {
            get => ((IDictionary)InternalDictionary)[key];
            set => this[(TKey)key] = (TValue)value;
        }
        void IDictionary.Add(object key, object value) => Add((TKey)key, (TValue)value);
        void IDictionary.Remove(object key) => Remove((TKey)key);
        bool IDictionary.Contains(object key) => InternalDictionary.ContainsKey((TKey)key);
        bool IDictionary.IsFixedSize => ((IDictionary)InternalDictionary).IsFixedSize;
        bool IDictionary.IsReadOnly => ((IDictionary)InternalDictionary).IsReadOnly;
        ICollection IDictionary.Keys => ((IDictionary)InternalDictionary).Keys;
        ICollection IDictionary.Values => ((IDictionary)InternalDictionary).Values;
        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)InternalDictionary).GetEnumerator();


        //ICollection
        /*
        void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex) => InternalCollection.CopyTo(array, arrayIndex);
        bool ICollection<TValue>.Contains(TValue item) => InternalCollection.Contains(item);
        bool ICollection<TValue>.IsReadOnly => InternalDictionary.IsReadOnly;
        void ICollection<TValue>.Add(TValue item) => throw new NotImplementedException("Adding an item without a key is not allowed.");

        bool ICollection<TValue>.Remove(TValue item) {
            if (!InternalCollection.Contains(item)) return false;
            foreach (var dictionaryItem in InternalDictionary) {
                if (item?.GetHashCode() != dictionaryItem.Value.GetHashCode()) continue;
                Remove(dictionaryItem.Key);
                return true;
            }
            return false;
        }
        */
        //Key Value operations
        public ICollection<TKey> Keys => InternalDictionary.Keys;
        public ICollection<TValue> Values => InternalDictionary.Values;
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => InternalDictionary.Contains(item);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => InternalDictionary.CopyTo(array, arrayIndex); bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => InternalDictionary.IsReadOnly;
        
        //Enumerator
        public IEnumerator<TValue> GetEnumerator() => InternalCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => InternalDictionary.GetEnumerator();
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => InternalDictionary.GetEnumerator();

        //ICollection
        void ICollection.CopyTo(Array array, int arrayIndex) => ((ICollection)InternalDictionary).CopyTo(array, arrayIndex);
        bool ICollection.IsSynchronized => ((ICollection)InternalDictionary).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)InternalDictionary).SyncRoot;

    }
}
