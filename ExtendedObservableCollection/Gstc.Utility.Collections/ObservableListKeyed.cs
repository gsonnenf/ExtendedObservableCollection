using System;
using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Base;


namespace Gstc.Collections.Observable {


    /// <summary>
    /// This class is an Observable List wherein items are accessed via index using a Key defined in the
    /// object, functioning somewhat similar to a Keyed Collection. Standard integer indexing can be used
    /// using the exposed List property.
    ///
    /// This class can be used to wrap a standard list via constructor.
    ///
    /// The class implements the INotifyCollectionChanged and INotifyDictionaryChanged providing notification
    /// on the change of objects.
    ///
    /// The class contains a backing List, wherein elements can be accessed via index, and a backing dictionary
    /// providing O(1) index access times. The dictionary key is defined by the GetKey method and created
    /// when added to the list. Care should be taken when updating the key property in the element, as it
    /// will not propogate to the dictionary.
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TItem"></typeparam>
    public abstract class ObservableListKeyed<TKey,TItem> : ObservableBaseDictionaryList<TKey, TItem> {

        //Backing list, and dictionary to store keyValuePairs for fast lookup
        private List<TItem> _list = new List<TItem>();
        private readonly Dictionary<TKey, TItem> _dictionary = new Dictionary<TKey, TItem>();


        protected ObservableListKeyed() {}

        protected ObservableListKeyed(List<TItem> list) {
            List = List;
        }

        /// <summary>
        /// This is the map between your object and its internal key. It must be overridden, or alternatively use the ObservableKeyedListFunc class,
        /// and supply it with a Delegate.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract TKey GetKey(TItem item);

        
        /// <summary>
        /// Sets the internal list the ObservableListKeyed is bound too.
        /// </summary>
        public List<TItem> List {
            get { return _list; }
            set {
                _list = value;
                _dictionary.Clear();
                foreach (var element in _list)   _dictionary.Add(GetKey(element),element);
                OnPropertyChangedIndexerCount();
                OnCollectionReset();
                OnDictionaryReset();
            }
        }
        
        /// <summary>
        /// Returns the internal dictionary that is used to track keys on the list. It is not advisable to edit.
        /// </summary>
        public Dictionary<TKey, TItem> Dictionary => _dictionary;

        public TItem GetOrDefault(TKey key) {
            TItem item;
            _dictionary.TryGetValue(key, out item);
            return item;
        }

        #region override properties for inherentance

        protected override IList<TItem> InternalList => _list;
        protected override IDictionary<TKey, TItem> InternalDictionary => _dictionary;

        #endregion
        
        #region override Methods

        public override TItem this[TKey key] {
            get {
                return _dictionary[key];
            }
            set {
                if (!_dictionary.ContainsKey(key)) Add(key, value);
                else {
                    var item = _dictionary[key];
                    var index = _list.IndexOf(item);
                    if (!Equals(key, GetKey(value))) throw new ArgumentException("Explicit Key must match Item Key.");
                    _dictionary[key] = value;
                    _list[index] = value;

                    OnPropertyChangedIndexer();
                    OnDictionaryReplace(key, item, value);
                    OnCollectionReplace(item, value, index);
                }
            }
        }

        public override void Add(TItem item) {
            if (_dictionary.ContainsKey(GetKey(item))) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(GetKey(item), item);
            _list.Add(item);

            OnPropertyChangedIndexerCount();
            OnCollectionAdd(item, _list.IndexOf(item));
            OnDictionaryAdd(GetKey(item), item);
        }

        public void AddRange(IList<TItem> items) {
            var count = _list.Count;
            _list.AddRange(items);
            OnPropertyChangedIndexerCount();
            OnCollectionAddEnumerable((IList)items, count);
        }

        public override void Add(TKey key, TItem item) {
            if (!Equals(key, GetKey(item))) throw new ArgumentException("Explicit Key must match Item Key.");
            if (_dictionary.ContainsKey(key)) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(GetKey(item), item);
            _list.Add(item);

            OnPropertyChangedIndexerCount();
            OnCollectionAdd(item, _list.IndexOf(item));
            OnDictionaryAdd(key, item);
        }

        


        public override void Clear() {
            _dictionary.Clear();
            _list.Clear();
            OnPropertyChangedIndexerCount();
            OnCollectionReset();
            OnDictionaryReset();
        }

        public override void Insert(int index, TItem item) {
            var key = GetKey(item);
            if (_dictionary.ContainsKey(key)) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(key, item);
            _list.Insert(index, item);

            OnPropertyChangedIndexerCount();
            OnCollectionAdd(item, index);
            OnDictionaryAdd(key, item);
        }

        public override void Move(int oldIndex, int newIndex) {
            TItem removedItem = this._list[oldIndex];

            _list.RemoveAt(oldIndex);
            _list.Insert(newIndex, removedItem);

            OnPropertyChangedIndexer();
            OnCollectionMove(removedItem, newIndex, oldIndex);
        }

        public override bool Remove(TKey key) {

            var item = _dictionary[key];
            if (item == null) return false;
            _dictionary.Remove(key);
            var index = _list.IndexOf(item);
            _list.RemoveAt(index);

            OnPropertyChangedIndexerCount();
            OnCollectionRemove(item, index);
            OnDictionaryRemove(key, item);
            return true;
        }

        public override bool Remove(TItem item) {
            var index = _list.IndexOf(item);

            if (index == -1) return false;
            var key = GetKey(item);          
            _list.RemoveAt(index);        
            _dictionary.Remove(key);

            OnPropertyChangedIndexerCount();
            OnCollectionRemove(item, index);
            OnDictionaryRemove(key, item);
            return true;
        }

        public override void RemoveAt(int index) {
            if (index >= Count) return;
            var item = List[index];
            var key = GetKey(item);
            _list.RemoveAt(index);
            _dictionary.Remove(key);
            OnPropertyChangedIndexerCount();
            OnCollectionRemove(item, index);
            OnDictionaryRemove(key, item);
        }
        #endregion
    }
}
