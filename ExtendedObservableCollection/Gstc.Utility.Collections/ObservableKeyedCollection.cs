using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable {

    /// <summary>
    /// Observable wrapper for Keyed Collection. Consider using an ObservableKeyedList if you need to change the base list after runtime or 
    /// you do not have access to the GetKeyForItem(..) used in the KeyedCollection. 
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObservableDictionaryListKeyedCollection<TKey, TValue> : ObservableBaseDictionaryList<TKey, TValue> {

        private KeyedCollection<TKey, TValue> _collection;

        public Func<TValue, TKey> GetKeyForItem;

        protected override IDictionary<TKey, TValue> InternalDictionary {
            get { throw new NotImplementedException("KeyedCollection does not implement IDictionary. Use an OnObservableKEyedList instead."); }
        }

        protected override IList<TValue> InternalList => _collection;

        #region Constructors

        /// <summary>
        /// Initialize with a KeyedCollection and with an equivlanet GetKeyForItem function the KeyedCollection uses to reference its keys.
        /// 
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="getKeyForItem"></param>
        protected ObservableDictionaryListKeyedCollection(KeyedCollection<TKey, TValue> collection, Func<TValue, TKey> getKeyForItem) {
            _collection = collection;
            GetKeyForItem = getKeyForItem;
        }
        #endregion

        #region Override Methods

        public override TValue this[TKey key] {
            get { return _collection[key]; }
            set {
                if (_collection.Contains(key)) {
                    CheckReentrancy();
                    var oldValue = _collection[key];
                    var oldIndex = _collection.IndexOf(oldValue);
                    _collection.Remove(oldValue);
                    _collection.Add(value);

                    OnPropertyChanged(IndexerName);
                    OnDictionaryReplace(key, oldValue, value);
                    OnCollectionRemove(oldValue, oldIndex);
                    OnCollectionAdd(value, _collection.IndexOf(value));
                } else Add(key, value);
            }
        }

        public override void Add(TValue value) {
            var key = GetKeyForItem(value);
            if (_collection.Contains(key)) throw new ArgumentException("Already contains an item with that key.");
            _collection.Add(value);
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionAdd(value, _collection.IndexOf(value));
            OnDictionaryAdd(key, value);
        }

        public override void Add(TKey key, TValue value) {

            var itemKey = GetKeyForItem(value);
            if (_collection.Contains(key)) throw new ArgumentException("Already contains an item with that key.");
            if (!Equals(key, itemKey)) throw new ArgumentException("Explicit Key does not match item's key");
            _collection.Add(value);
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionAdd(value, _collection.IndexOf(value));
            OnDictionaryAdd(key, value);
        }

        public override void Clear() {
            CheckReentrancy();
            _collection.Clear();
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionReset();
            OnDictionaryReset();
        }

        public override void Insert(int index, TValue item) {
            var oldItem = _collection[index];
            var oldItemKey = GetKeyForItem(oldItem);
            var newItemKey = GetKeyForItem(item);

            _collection.Insert(index, item);

            OnPropertyChangedIndexer();
            OnCollectionReplace(oldItem, item, index);
            if (Equals(oldItemKey, newItemKey)) OnDictionaryReplace(newItemKey, oldItem, item);
            else {
                OnDictionaryRemove(oldItemKey, oldItem);
                OnDictionaryAdd(newItemKey, item);
            }
        }

        public override void Move(int oldIndex, int newIndex) {

            TValue removedItem = _collection[oldIndex];

            _collection.RemoveAt(oldIndex);
            _collection.Insert(newIndex, removedItem);

            OnPropertyChangedIndexer();
            OnCollectionAdd(removedItem, oldIndex);
        }


        public override bool Remove(TKey key) {
            var removedValue = _collection[key];
            var removedIndex = _collection.IndexOf(removedValue);

            if (!_collection.Remove(_collection[key])) return false;
            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionRemove(removedValue, removedIndex);
            OnDictionaryRemove(key, removedValue);
            return true;
        }

        public override bool Remove(TValue value) {
            var removedIndex = _collection.IndexOf(value);
            var removedValue = value;
            var removedKey = GetKeyForItem(value);
            if (!_collection.Remove(value)) return false;

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionRemove(removedValue, removedIndex);
            OnDictionaryRemove(removedKey, removedValue);
            return true;
        }

        public override void RemoveAt(int index) {
            var item = _collection[index];
            var key = GetKeyForItem(item);
            _collection.RemoveAt(index);

            OnPropertyChanged(CountString);
            OnPropertyChanged(IndexerName);
            OnCollectionRemove(item, index);
            OnDictionaryRemove(key, item);
        }



        #endregion
    }
}
