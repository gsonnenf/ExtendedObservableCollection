using Gstc.Collections.Observable.Base;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Gstc.Collections.Observable.Extended {


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
    /// <typeparam name="TValue"></typeparam>
    public abstract class ObservableListKeyed<TKey, TValue> : BaseObservableListDictionary<TKey, TValue> {

        //Backing list, and dictionary to store keyValuePairs for fast lookup
        private List<TValue> _list;
        private readonly Dictionary<TKey, TValue> _dictionary = new Dictionary<TKey, TValue>();

        protected override IList<TValue> InternalList => _list;
        protected override IDictionary<TKey, TValue> InternalDictionary => _dictionary;


        //Constructors
        protected ObservableListKeyed() { _list = new List<TValue>(); }

        protected ObservableListKeyed(List<TValue> list) { List = list; }

        //Properties

        /// <summary>
        /// This is the map between your object and its internal key. It must be overridden, or alternatively use the ObservableKeyedListFunc class,
        /// and supply it with a Delegate.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public abstract TKey GetKey(TValue item);


        /// <summary>
        /// Sets the internal list the ListKeyed is bound too.
        /// </summary>
        public List<TValue> List {
            get => _list;
            set {
                _list = value;
                _dictionary.Clear();
                foreach (var element in _list) _dictionary.Add(GetKey(element), element);
                OnPropertyChangedCountAndIndex();
                OnCollectionChangedReset();
                OnDictionaryReset();
            }
        }

        /// <summary>
        /// Returns the internal dictionary that is used to track keys on the list.
        /// </summary>
        public Dictionary<TKey, TValue> Dictionary => _dictionary;

        public TValue GetOrDefault(TKey key) {
            _dictionary.TryGetValue(key, out var item);
            return item;
        }

        //Override
        public override TValue this[int index] {
            get => InternalList[index];
            set {
                TValue item = value;
                TKey key = GetKey(value);
                if (_dictionary.ContainsKey(key)) {
                    TValue oldItem = _dictionary[key];
                    _dictionary[key] = item;
                    _list[index] = item;
                    OnPropertyChangedIndex();
                    OnCollectionChangedReplace(oldItem, item, index);
                    OnDictionaryReplace(key, oldItem, item);
                } else {
                    //TODO: Fix for cases of adding vs. replacing. Index or IndexAndProperty, change replaced or added

                    _dictionary[key] = item;
                    _list[index] = item;
                    OnPropertyChangedIndex();
                    OnCollectionChangedAdd(item, _list.IndexOf(item));
                    OnDictionaryChangedAdd(key, item);
                }
            }
        }

        public override TValue this[TKey key] {
            get => _dictionary[key];
            set {
                if (!_dictionary.ContainsKey(key)) Add(key, value);
                else {
                    var item = _dictionary[key];
                    var index = _list.IndexOf(item);
                    if (!Equals(key, GetKey(value))) throw new ArgumentException("Explicit Key must match Item Key.");
                    _dictionary[key] = value;
                    _list[index] = value;

                    OnPropertyChangedIndex();
                    OnDictionaryReplace(key, item, value);
                    OnCollectionChangedReplace(item, value, index);
                }
            }
        }

        public override void Add(TValue item) {
            if (_dictionary.ContainsKey(GetKey(item))) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(GetKey(item), item);
            _list.Add(item);

            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAdd(item, _list.IndexOf(item));
            OnDictionaryChangedAdd(GetKey(item), item);
        }

        public void AddRange(IList<TValue> items) {
            var count = _list.Count;
            _list.AddRange(items);
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAddMany((IList)items, count);
        }

        public override void Add(TKey key, TValue item) {
            if (!Equals(key, GetKey(item))) throw new ArgumentException("Explicit Key must match Item Key.");
            if (_dictionary.ContainsKey(key)) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(GetKey(item), item);
            _list.Add(item);

            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAdd(item, _list.IndexOf(item));
            OnDictionaryChangedAdd(key, item);
        }

        public override void Clear() {
            _dictionary.Clear();
            _list.Clear();
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedReset();
            OnDictionaryReset();
        }

        public override void Insert(int index, TValue item) {
            var key = GetKey(item);
            if (_dictionary.ContainsKey(key)) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(key, item);
            _list.Insert(index, item);

            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAdd(item, index);
            OnDictionaryChangedAdd(key, item);
        }

        public override void Move(int oldIndex, int newIndex) {
            var removedItem = _list[oldIndex];

            _list.RemoveAt(oldIndex);
            _list.Insert(newIndex, removedItem);

            OnPropertyChangedIndex();
            OnCollectionChangedMove(removedItem, newIndex, oldIndex);
        }

        public override bool Remove(TKey key) {

            var item = _dictionary[key];
            if (item == null) return false;
            _dictionary.Remove(key);
            var index = _list.IndexOf(item);
            _list.RemoveAt(index);

            OnPropertyChangedCountAndIndex();
            OnCollectionChangedRemove(item, index);
            OnDictionaryRemove(key, item);
            return true;
        }

        public override bool Remove(TValue item) {
            var index = _list.IndexOf(item);

            if (index == -1) return false;
            var key = GetKey(item);
            _list.RemoveAt(index);
            _dictionary.Remove(key);

            OnPropertyChangedCountAndIndex();
            OnCollectionChangedRemove(item, index);
            OnDictionaryRemove(key, item);
            return true;
        }

        public override void RemoveAt(int index) {
            if (index >= Count) return;
            var item = List[index];
            var key = GetKey(item);
            _list.RemoveAt(index);
            _dictionary.Remove(key);
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedRemove(item, index);
            OnDictionaryRemove(key, item);
        }
    }
}
