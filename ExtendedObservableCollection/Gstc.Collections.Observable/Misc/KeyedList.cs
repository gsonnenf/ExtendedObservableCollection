using System;
using System.Collections;
using System.Collections.Generic;

namespace Gstc.Collections.Observable.misc {
    public abstract class KeyedList<TKey, TValue> : IList<TValue> {

        private List<TValue> _list;
        private Dictionary<TKey, TValue> _dictionary;
        public List<TValue> List { get; set; }
        public Dictionary<TKey, TValue> Dictionary => _dictionary;

        public abstract TKey GetKey(TValue item);

        public void Clear() {
            _dictionary.Clear();
            _list.Clear();
        }

        public void Add(TValue item) {
            if (_dictionary.ContainsKey(GetKey(item))) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(GetKey(item), item);
            _list.Add(item);
        }

        public void Add(TKey key, TValue item) {
            if (!ValidateKey(key, item)) throw new ArgumentException("Explicit Key must match Item Key.");
            if (_dictionary.ContainsKey(key)) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(GetKey(item), item);
            _list.Add(item);
        }

        public void Insert(int index, TValue item) {
            var key = GetKey(item);
            if (_dictionary.ContainsKey(key)) throw new ArgumentException("Duplicate Keyed items are not allowed.");
            _dictionary.Add(key, item);
            _list.Insert(index, item);
        }

        public bool Remove(TKey key) {
            var item = _dictionary[key];
            if (item == null) return false;
            _dictionary.Remove(key);
            _list.Remove(item);
            return true;
        }

        public bool Remove(TValue item) {
            if (!_list.Remove(item)) return false;
            _dictionary.Remove(GetKey(item));
            return true;
        }

        public void RemoveAt(int index) {
            var item = _list[index];
            var key = GetKey(item);
            _list.RemoveAt(index);
            _dictionary.Remove(key);
        }

        public TValue this[TKey key] {
            get { return _dictionary[key]; }
            set {
                if (!ValidateKey(key, value)) throw new ArgumentException("Explicit key must Match Item.");
                if (_dictionary.ContainsKey(key)) {
                    var oldItem = _dictionary[key];
                    var index = _list.IndexOf(oldItem);
                    _dictionary[key] = value;
                    _list[index] = value;
                } else {
                    _list.Add(value);
                    _dictionary.Add(GetKey(value), value);
                }
            }
        }

        // Pass through functions
        public bool Contains(TValue item) => _list.Contains(item);

        public bool Contains(TKey key) => _dictionary.ContainsKey(key);

        public void CopyTo(TValue[] array, int arrayIndex) => _list.CopyTo(array, arrayIndex);

        public int IndexOf(TValue item) => _list.IndexOf(item);

        public int Count => _list.Count;
        public bool IsReadOnly => ((IList)_list).IsReadOnly;

        TValue IList<TValue>.this[int index] {
            get { return _list[index]; }
            set {
                var oldKey = GetKey(_list[index]);
                this[oldKey] = value;
            }
        }

        public IEnumerator<TValue> GetEnumerator() => _list.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => _list.GetEnumerator();

        public bool ValidateKey(TKey key, TValue item) => (Equals(key, GetKey(item)));


    }
}
