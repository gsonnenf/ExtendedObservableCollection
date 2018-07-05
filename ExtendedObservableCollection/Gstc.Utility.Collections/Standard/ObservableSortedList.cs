using System;
using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable.Standard {

    /// <summary>
    /// This class is a wrapper for a sorted list that implements INotifyCollectionChanged and INotifyDictionaryChanged.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObservableSortedList<TKey, TValue> : BaseObservableSortedList<TKey, TValue> {
       
        private SortedList<TKey, TValue> _sortedList;

        protected override SortedList<TKey, TValue> InternalSortedList => _sortedList;

        public SortedList<TKey, TValue> SortedList {
            get => _sortedList;
            set {
                _sortedList = value;
                OnPropertyChangedCountAndIndex();
                OnCollectionChangedReset();
                OnDictionaryReset();
            }
        }

        //Constructors
        public ObservableSortedList() {  _sortedList = new SortedList<TKey, TValue>();  }

        public ObservableSortedList(SortedList<TKey, TValue> sortedList) { SortedList = sortedList;  }     

        // Overrides
        public override TValue this[TKey key] {
            get => _sortedList[key];
            set {
                if (ContainsKey(key)) {
                    //CheckReentrancy();
                    var oldValue = _sortedList[key];
                    _sortedList[key] = value;
                    OnPropertyChangedIndex();
                    OnDictionaryReplace(key, oldValue, value);
                    OnCollectionChangedReplace(oldValue, value, _sortedList.IndexOfKey(key));
                } else Add(key, value);
            }
        }

        public override void Add(TKey key, TValue value) {
            //CheckReentrancy();
            _sortedList.Add(key, value);           
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAdd(value, _sortedList.IndexOfKey(key) );
            OnDictionaryChangedAdd(key, value);
        }
   
        public override void Clear() {
            //CheckReentrancy();
            _sortedList.Clear();
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedReset();
            OnDictionaryReset();
        }

        public override bool Remove(TKey key) {
            //CheckReentrancy();           
            var removedIndex = _sortedList.IndexOfKey(key);
            var removedValue = _sortedList[key];
            if ( !_sortedList.Remove(key) ) return false;
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedRemove(removedValue, removedIndex);
            OnDictionaryRemove(key, removedValue);

            return true;
        }

    }
    
}
