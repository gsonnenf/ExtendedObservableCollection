using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Collections.Observable.Base {
    public abstract class ObservableBaseCollection<TItem> : 
        ObservableNotifyCollectionBase, 
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
