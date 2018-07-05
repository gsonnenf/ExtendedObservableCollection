﻿using System;
using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Interface;
using Gstc.Collections.Observable.Notify;

namespace Gstc.Collections.Observable.Base {

    //TODO: Rename as ObservableIDictionaryBase and break out into several different things
    public abstract class BaseObservableListDictionary<TKey, TValue> :       
        NotifyDictionaryCollection<TKey, TValue>,        
        IObservableDictionaryCollection<TKey,TValue>,
        IObservableList<TValue> {

        protected abstract IList<TValue> InternalList { get; }
        protected abstract IDictionary<TKey, TValue> InternalDictionary { get; }

        public abstract TValue this[int index] { get; set; }
        public abstract void Insert(int index, TValue item); //TODO: Make sure you did this one right
        public abstract void RemoveAt(int index);
        public abstract void Add(TValue item);
        public abstract void Clear();
        public abstract bool Remove(TValue item);
        public abstract void Move(int oldIndex, int newIndex);

        //List
        public int Count => InternalList.Count;
        public int IndexOf(TValue item) => InternalList.IndexOf(item);

        //Dictionary
        public abstract TValue this[TKey key] { get; set; }
        public abstract void Add(TKey key, TValue value);
        public abstract bool Remove(TKey key);

        //Dictionary
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



    }
}