using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gstc.Collections.Observable.Interface;

namespace Gstc.Collections.Observable.Base {
    public abstract class ObservableBaseListKey<TKey,TValue> : 
        ObservableNotifyDictionaryCollectionBase<TKey,TValue>,
        IObservableList<TValue> {

        protected abstract IList<TValue> InternalList { get; }
        
        public abstract void Insert(int index, TValue item);
        public abstract void RemoveAt(int index);       
        public abstract void Add(TValue item);
        public abstract void Move(int oldIndex, int newIndex);
        public abstract void Clear();
        public abstract bool Remove(TValue item);

        #region IList impl.

        TValue IList<TValue>.this[int index] {
            get { return InternalList[index]; }
            set { Insert(index, value); }
        }

        public int Count => InternalList.Count;
        public bool IsReadOnly => ((IList)InternalList).IsReadOnly;
        public bool Contains(TValue item) => InternalList.Contains(item);
        public void CopyTo(TValue[] array, int arrayIndex) => InternalList.CopyTo(array, arrayIndex);
        public int IndexOf(TValue item) => InternalList.IndexOf(item);
        public IEnumerator<TValue> GetEnumerator() => InternalList.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => InternalList.GetEnumerator();
        
        #endregion
    }
}
