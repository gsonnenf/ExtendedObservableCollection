using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Interface;
using Gstc.Collections.Observable.Notify;

namespace Gstc.Collections.Observable.Base {

    public abstract class BaseObservableList<TItem> : 
        NotifyCollection,
        IObservableList<TItem> {

        #region abstract methods
        protected abstract IList<TItem> InternalList { get; }
        public abstract TItem this[int index] { get; set; }
        public abstract void Insert(int index, TItem item); //TODO: Make sure you did this one right
        public abstract void RemoveAt(int index);
        public abstract void Add(TItem item);
        public abstract void Clear();
        public abstract bool Remove(TItem item);
        public abstract void Move(int oldIndex, int newIndex);
        #endregion

        TItem IList<TItem>.this[int index] {
            get => this[index];
            set => this[index] = value;
        }

        public int Count => InternalList.Count;
        
        public bool Contains(TItem item) => InternalList.Contains(item);
        public void CopyTo(TItem[] array, int arrayIndex) => InternalList.CopyTo(array, arrayIndex);
        public int IndexOf(TItem item) => InternalList.IndexOf(item);
        public IEnumerator<TItem> GetEnumerator() => InternalList.GetEnumerator();
        bool ICollection<TItem>.IsReadOnly => InternalList.IsReadOnly;
        IEnumerator IEnumerable.GetEnumerator() => InternalList.GetEnumerator();

       
    }
}
