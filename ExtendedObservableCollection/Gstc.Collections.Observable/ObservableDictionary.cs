﻿using Gstc.Collections.Observable.Base;
using System.Collections.Generic;

namespace Gstc.Collections.Observable {

    /// <summary>
    /// Observable dictionary can serve as a stand alone dictionary, or serve as an observable wrapper for a pre-existing dictionary.
    /// ObservableDictionary implements INotifyPropertyChanged and INotifyDictionaryChanged. It does NOT implement ICollectionChanged.
    /// Use an ObservableKeyedCollection or SortedList if you need ICollectionChanged.
    /// 
    /// </summary>
    /// <typeparam name="TKey">Key field of Dictionary</typeparam>
    /// <typeparam name="TValue">Value field of Dictionary</typeparam>
    public class ObservableDictionary<TKey, TValue> : BaseObservableDictionary<TKey, TValue> {

        private Dictionary<TKey, TValue> _dictionary;

        public ObservableDictionary() {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionary(Dictionary<TKey, TValue> dictionary) {
            Dictionary = dictionary;
        }

        public Dictionary<TKey, TValue> Dictionary {
            get => _dictionary;
            set {
                _dictionary = value;
                OnPropertyChanged(CountString);
                OnPropertyChanged(IndexerName);
                OnDictionaryReset();
            }
        }

        protected override IDictionary<TKey, TValue> InternalDictionary => Dictionary;

        public override TValue this[TKey key] {
            get => _dictionary[key];
            set {
                CheckReentrancy();
                if (!ContainsKey(key)) {
                    Add(key, value);
                    return;
                }
                var oldValue = _dictionary[key];
                var newValue = value;
                _dictionary[key] = newValue;
                OnPropertyChanged(IndexerName);
                OnDictionaryReplace(key, oldValue, newValue);
            }
        }

        public override void Add(TKey key, TValue value) {
            CheckReentrancy();
            _dictionary.Add(key, value);
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnDictionaryAdd(key, value);
        }

        public override void Clear() {
            CheckReentrancy();
            _dictionary.Clear();
            OnPropertyChangedCountAndIndex();
            OnDictionaryReset();
        }

        public override bool Remove(TKey key) {
            CheckReentrancy();
            var removedItem = _dictionary[key];
            if (!_dictionary.Remove(key)) return false;
            OnPropertyChangedCountAndIndex();
            OnDictionaryRemove(key, removedItem);
            return true;
        }
    }
}
