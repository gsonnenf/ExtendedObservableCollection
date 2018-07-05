using System.Collections.Generic;
using System.Collections.ObjectModel;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable.unfinished {


    /// <summary>
    /// An observable Dictionary with a backing list and observable Icollection.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObservableDictionaryCollection<TKey,TValue> : BaseObservableDictionaryCollection<TKey,TValue> {

        private IDictionary<TKey, TValue> _dictionary;
        private Collection<TValue> _collection = new Collection<TValue>();

        protected override ICollection<TValue> InternalCollection => _collection;
        protected override IDictionary<TKey, TValue> InternalDictionary => _dictionary;

        //Constructors
        public ObservableDictionaryCollection() {
            _dictionary = new Dictionary<TKey, TValue>();
        }

        public ObservableDictionaryCollection(Dictionary<TKey, TValue> dictionary) {
            Dictionary = _dictionary;
        }
      
        //Properties
        public IDictionary<TKey, TValue> Dictionary {
            get => _dictionary;
            set {
                _dictionary = value;
                _collection.Clear();
                
                var list = new List<TValue>();
                foreach (var item in Dictionary) {                  
                    list.Add(item.Value);
                    _collection.Add(item.Value);
                }
                OnDictionaryReset();
                OnCollectionChangedReset();
                OnPropertyChangedCountAndIndex();
            }
        }

        //Overrides
        public override TValue this[TKey key] {
            get => Dictionary[key];
            set {
                var item = value;
                if (_dictionary.ContainsKey(key)) {
                    var oldItem = _dictionary[key];
                    var index = _collection.IndexOf(item);
                    _dictionary[key] = item;
                    _collection[index] = item;
                    OnPropertyChangedIndex();
                    OnCollectionChangedReplace(oldItem, item, index);
                    OnDictionaryReplace(key, oldItem, item);
                } else {
                    _dictionary[key] = item;
                    _collection.Add(item);
                    OnPropertyChangedCountAndIndex();
                    OnCollectionChangedAdd(item, _collection.IndexOf(item));
                    OnDictionaryChangedAdd(key, item);
                }
            }
        }

        public override void Add(TKey key, TValue value) {
            _dictionary.Add(key, value);
            _collection.Add(value);
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAdd(value, _collection.IndexOf(value));
            OnDictionaryChangedAdd(key, value);
        }

        public override bool Remove(TKey key) {
            if (!_dictionary.ContainsKey(key)) return false;
            var item = _dictionary[key];

            var index = _collection.IndexOf(item);
            _collection.RemoveAt(index);           
            _dictionary.Remove(key);

            OnPropertyChangedCountAndIndex();
            OnCollectionChangedRemove(item,index);
            OnDictionaryRemove(key,item);

            return true;
        }

        public override void Clear() {
            _dictionary.Clear();
            _collection.Clear();
            OnCollectionChangedReset();
            OnDictionaryReset();
        }
    }
}
