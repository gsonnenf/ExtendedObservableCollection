using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Gstc.Collections.Observable.Base {
    public abstract class BaseObservableSortedList<TKey, TValue> :

        NotifyDictionaryCollection<TKey, TValue>,
        IObservableCollection,

        IDictionary<TKey, TValue>, ICollection<KeyValuePair<TKey, TValue>>,
        IEnumerable<KeyValuePair<TKey, TValue>>, IDictionary, ICollection, IEnumerable {

        protected abstract SortedList<TKey, TValue> InternalSortedList { get; }

        public abstract TValue this[TKey key] { get; set; }
        public abstract void Clear();
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);


        public int Count => InternalSortedList.Count;
        public bool ContainsKey(TKey key) => InternalSortedList.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => InternalSortedList.TryGetValue(key, out value);

        //IDictionary
        void IDictionary.Add(object obj1, object obj2) => Add((TKey)obj1, (TValue)obj2);
        void IDictionary.Remove(object obj) => Remove((TKey)obj);

        public object this[object key] {
            get => this[(TKey)key];
            set => this[(TKey)key] = (TValue)value;
        }

        bool IDictionary.IsFixedSize => ((IDictionary)InternalSortedList).IsFixedSize;
        bool IDictionary.IsReadOnly => ((IDictionary)InternalSortedList).IsReadOnly;
        bool IDictionary.Contains(object obj) => ((IDictionary)InternalSortedList).Contains(obj);

        ICollection IDictionary.Keys => ((IDictionary)InternalSortedList).Keys;
        ICollection IDictionary.Values => ((IDictionary)InternalSortedList).Values;

        //ICollection
        void ICollection.CopyTo(Array array, int arrayIndex) => ((ICollection)InternalSortedList).CopyTo(array, arrayIndex);
        bool ICollection.IsSynchronized => ((ICollection)InternalSortedList).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)InternalSortedList).SyncRoot;


        //Key Value operations
        public ICollection<TKey> Keys => InternalSortedList.Keys;
        public ICollection<TValue> Values => InternalSortedList.Values;
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => InternalSortedList.Contains(item);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => ((ICollection<KeyValuePair<TKey, TValue>>)InternalSortedList).CopyTo(array, arrayIndex);
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => ((ICollection<KeyValuePair<TKey, TValue>>)InternalSortedList).IsReadOnly;

        //Enumerator
        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)InternalSortedList).GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => InternalSortedList.GetEnumerator();
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => ((IDictionary<TKey, TValue>)InternalSortedList).GetEnumerator();
    }
}
