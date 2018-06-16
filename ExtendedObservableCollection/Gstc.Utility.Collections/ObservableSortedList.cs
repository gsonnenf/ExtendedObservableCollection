using System;
using System.Collections.Generic;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable {

    /// <summary>
    /// This class is a wrapper for a sorted list that implements INotifyCollectionChanged and INotifyDictionaryChanged.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObservableSortedList<TKey, TValue> : ObservableBaseDictionaryCollection<TKey, TValue> {
       
        private SortedList<TKey, TValue> _sortedList;
        protected override IDictionary<TKey, TValue> InternalDictionary => _sortedList;

        #region constructor

        public ObservableSortedList() {
            _sortedList = new SortedList<TKey, TValue>();
        }
        public ObservableSortedList(SortedList<TKey, TValue> sortedList) {
            _sortedList = sortedList;
        }
        
        #endregion

        public SortedList<TKey, TValue> SortedList {
            get { return _sortedList;}
            set {
                _sortedList = value;
                OnPropertyChangedIndexerCount();
                OnCollectionReset();
                OnDictionaryReset();
            }
        }
     

        #region override methods
        public override TValue this[TKey key] {
            get { return _sortedList[key]; }
            set {
                if (ContainsKey(key)) {
                    CheckReentrancy();
                    var oldValue = _sortedList[key];
                    _sortedList[key] = value;
                    OnPropertyChangedIndexer();
                    OnDictionaryReplace(key, oldValue, value);
                    OnCollectionReplace(oldValue, value, _sortedList.IndexOfKey(key));
                } else Add(key, value);
            }
        }

        public override void Add(TKey key, TValue value) {
            CheckReentrancy();
            _sortedList.Add(key, value);           
            OnPropertyChangedIndexerCount();
            OnCollectionAdd(value, _sortedList.IndexOfKey(key) );
            OnDictionaryAdd(key, value);
        }
   

        public override void Clear() {
            CheckReentrancy();
            _sortedList.Clear();
            OnPropertyChangedIndexerCount();
            OnCollectionReset();
            OnDictionaryReset();
        }

        public override bool Remove(TKey key) {
            CheckReentrancy();           
            var removedIndex = _sortedList.IndexOfKey(key);
            var removedValue = _sortedList[key];
            //var index = ((ICollection)Dictionary)).
            if (!_sortedList.Remove(key)) return false;
            OnPropertyChangedIndexerCount();
            OnCollectionRemove(removedValue, removedIndex);
            OnDictionaryRemove(key, removedValue);

            return true;
        }
       
        #endregion
    }
    
}
