using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Interface;
using Gstc.Collections.Observable.Notify;

namespace Gstc.Collections.Observable.Base {
    public abstract class BaseObservableCollection<TItem> : 
        NotifyCollectionBase, 
        IObservableCollection<TItem> {

        protected abstract ICollection<TItem> InternalCollection { get; }
        //public abstract TItem this[int index] { get; set; }
        public abstract void Add(TItem item);
        public abstract void Clear();
        public abstract bool Remove(TItem item);
      
        public int Count => InternalCollection.Count;
        public bool IsReadOnly => ((IList)InternalCollection).IsReadOnly;
        public bool Contains(TItem item) => InternalCollection.Contains(item);
        public void CopyTo(TItem[] array, int arrayIndex) => InternalCollection.CopyTo(array, arrayIndex);
        public IEnumerator<TItem> GetEnumerator() => InternalCollection.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => InternalCollection.GetEnumerator();
    }
}
