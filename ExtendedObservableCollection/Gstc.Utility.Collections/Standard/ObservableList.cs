using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable.Standard {

    /// <summary>
    /// This class is a wrapper for a List that implements IList, INotifyCollectionChanged and contains
    /// additional utility functions. It can be instantated on its own, or instantied with an already
    /// existing List. In many cases using ObservableList may be preffered over using the .NET ObservableCollection
    /// for its compatiblity with existing collection types and interface.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class ObservableList<TItem> : BaseObservableList<TItem> {

        private List<TItem> _list;
        protected override IList<TItem> InternalList => _list;

        //Constructors
        public ObservableList() { _list = new List<TItem>(); }

        public ObservableList(List<TItem> list) { List = list; }

        //Properties
        public List<TItem> List {
            get => _list;
            set {
                _list = value;
                OnPropertyChangedCountAndIndex();
                OnCollectionChangedReset();
            }
        }

        //Overrides
        public override TItem this[int index] {
            get => _list[index];
            set {
                var oldItem = _list[index];
                _list[index] = value;
                OnPropertyChangedIndex();
                OnCollectionChangedReplace(oldItem, value, index);
            }
        }

        public override void Add(TItem item) {
            _list.Add(item);
            OnPropertyChangedCountAndIndex(); 
            OnCollectionChangedAdd(item, _list.IndexOf(item));
        }

        public void AddRange(IList<TItem> items) {
            var count = _list.Count;
            _list.AddRange(items);
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAddMany( (IList)items, count);
        }

        public override void Clear() {
            _list.Clear();
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedReset();
        }

        public override void Insert(int index, TItem item) {
            _list.Insert(index,item);
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedAdd(item,index);
        }

        public override void Move(int oldIndex, int newIndex) {
            var removedItem = this[oldIndex];
            _list.RemoveAt(oldIndex);
            _list.Insert(newIndex, removedItem);
            OnPropertyChangedIndex();
            OnCollectionChangedMove(removedItem, oldIndex, newIndex);
        }

        public override bool Remove(TItem item) {
            var index = _list.IndexOf(item);
            if (index == -1) return false;
            _list.RemoveAt(index);
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedRemove(item, index);
            return true;
        }

        public override void RemoveAt(int index) {
            var item = _list[index];
            _list.RemoveAt(index);
            OnPropertyChangedCountAndIndex();
            OnCollectionChangedRemove(item, index);
        }
    }
}
