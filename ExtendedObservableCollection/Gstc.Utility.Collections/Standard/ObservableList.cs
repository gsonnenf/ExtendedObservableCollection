using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable {

    /// <summary>
    /// This class is a wrapper for a List that implements IList, INotifyCollectionChanged and contains
    /// additional utility functions. It can be instantated on its own, or instantied with an already
    /// existing List. In many cases using ObservableList may be preffered over using the .NET ObservableCollection
    /// for its compatiblity with existing collection types and interface.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class ObservableList<TItem> : BaseObservableList<TItem> {

        public ObservableList() {}

        public ObservableList(List<TItem> list) {
            List = list;
        }

        private List<TItem> _list = new List<TItem>();

        protected override IList<TItem> InternalList => _list;

        public List<TItem> List {
            get { return _list; }
            set {
                _list = value;
                OnPropertyChangedIndexerCount();
                OnCollectionReset();
            }
        }

        #region Overrides

        public override TItem this[int index] {
            get { return _list[index]; }
            set {
                var oldItem = _list[index];
                _list[index] = value;
                OnPropertyChangedIndexer();
                OnCollectionReplace(oldItem, value, index);
            }
        }

        public override void Add(TItem item) {
            _list.Add(item);
            OnPropertyChangedIndexerCount(); 
            OnCollectionAdd(item, _list.IndexOf(item));
        }

        public void AddRange(IList<TItem> items) {
            var count = _list.Count;
            _list.AddRange(items);
            OnPropertyChangedIndexerCount();
            OnCollectionAddEnumerable( (IList)items, count);
        }

        public override void Clear() {
            _list.Clear();
            OnPropertyChangedIndexerCount();
            OnCollectionReset();
        }

        public override void Insert(int index, TItem item) {
            _list.Insert(index,item);
            OnPropertyChangedIndexerCount();
            OnCollectionAdd(item,index);
        }

        public override void Move(int oldIndex, int newIndex) {
            TItem removedItem = this[oldIndex];

            _list.RemoveAt(oldIndex);
            _list.Insert(newIndex, removedItem);

            OnPropertyChangedIndexer();
            OnCollectionMove(removedItem, oldIndex, newIndex);
        }

        public override bool Remove(TItem item) {
            var index = _list.IndexOf(item);
            if (index == -1) return false;
            _list.RemoveAt(index);
            OnPropertyChangedIndexerCount();
            OnCollectionRemove(item, index);
            return true;
        }

        public override void RemoveAt(int index) {
            var item = _list[index];
            _list.RemoveAt(index);
            OnPropertyChangedIndexerCount();
            OnCollectionRemove(item, index);
        }

        #endregion
    }
}
