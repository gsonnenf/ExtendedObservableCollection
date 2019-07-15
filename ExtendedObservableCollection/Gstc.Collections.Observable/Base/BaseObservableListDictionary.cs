using System;
using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable;

namespace Gstc.Collections.Observable.Base {

    //TODO: Rename as ObservableIDictionaryBase and break out into several different things
    public abstract class BaseObservableListDictionary<TKey, TValue> :       
        NotifyDictionaryCollection<TKey, TValue>,        
        IObservableDictionaryCollection<TKey,TValue>,
        IObservableList<TValue> {
        
        protected abstract IList<TValue> InternalList { get; }
        protected abstract IDictionary<TKey, TValue> InternalDictionary { get; }

        public abstract TValue this[int index] { get; set; }
        public abstract void Insert(int index, TValue item);
        public abstract void RemoveAt(int index);
        public abstract void Add(TValue item);
        public abstract void Clear();
        public abstract bool Remove(TValue item);
        public abstract void Move(int oldIndex, int newIndex);

        //Dictionary<>
        public abstract TValue this[TKey key] { get; set; }
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);
        
        //List
        public int Count => InternalList.Count;
        public int IndexOf(TValue item) => InternalList.IndexOf(item);

        //Dictionary
        object IDictionary.this[object key] {
            get => ((IDictionary)InternalDictionary)[key];
            set => this[(TKey)key] = (TValue)value;
        }
        void IDictionary.Add(object key, object value) => Add((TKey)key, (TValue)value);
        void IDictionary.Remove(object key) => Remove((TKey) key);
        bool IDictionary.Contains(object key) => InternalDictionary.ContainsKey((TKey) key);
        bool IDictionary.IsFixedSize => ((IDictionary) InternalDictionary).IsFixedSize;
        bool IDictionary.IsReadOnly => ((IDictionary)InternalDictionary).IsReadOnly;
        ICollection IDictionary.Keys => ((IDictionary) InternalDictionary).Keys;
        ICollection IDictionary.Values => ((IDictionary)InternalDictionary).Values;
        IDictionaryEnumerator IDictionary.GetEnumerator() => ((IDictionary)InternalDictionary).GetEnumerator();

        //Dictionary<>
        public bool ContainsKey(TKey key) => InternalDictionary.ContainsKey(key);
        public bool TryGetValue(TKey key, out TValue value) => InternalDictionary.TryGetValue(key, out value);

        //Icollection
        void ICollection<TValue>.CopyTo(TValue[] array, int arrayIndex) => InternalList.CopyTo(array, arrayIndex);
        bool ICollection<TValue>.Contains(TValue item) => InternalList.Contains(item);
        bool ICollection<TValue>.IsReadOnly => InternalList.IsReadOnly;
        void ICollection<TValue>.Add(TValue item) => Add(item);
        bool ICollection<TValue>.Remove(TValue item) => Remove(item);
  
        //Key Value operations
        public ICollection<TKey> Keys => InternalDictionary.Keys;
        public ICollection<TValue> Values => InternalDictionary.Values;
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item) => Add(item.Key, item.Value);
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item) => Remove(item.Key);
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> item) => InternalDictionary.Contains(item);
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => InternalDictionary.CopyTo(array, arrayIndex); bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly => InternalDictionary.IsReadOnly;

        //Enumerator
        public IEnumerator<TValue> GetEnumerator() => InternalList.GetEnumerator();
        

        IEnumerator IEnumerable.GetEnumerator() => InternalList.GetEnumerator();
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() => InternalDictionary.GetEnumerator();


        //ICollection
        void ICollection.CopyTo(Array array, int arrayIndex) => ((ICollection)InternalList).CopyTo(array, arrayIndex);
        bool ICollection.IsSynchronized => ((ICollection)InternalList).IsSynchronized;
        object ICollection.SyncRoot => ((ICollection)InternalList).SyncRoot;








        //IList
        public bool IsReadOnly => throw new NotImplementedException();

        public bool IsFixedSize => throw new NotImplementedException();

        object IList.this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        int IList.Add(object value) => throw new NotImplementedException();
        bool IList.Contains(object value) => throw new NotImplementedException();
        int IList.IndexOf(object value) => throw new NotImplementedException();
        void IList.Insert(int index, object value) => throw new NotImplementedException();
        void IList.Remove(object value) => throw new NotImplementedException();

    }
}
