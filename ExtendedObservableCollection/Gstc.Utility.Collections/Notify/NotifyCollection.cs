using System.Collections;
using System.Collections.Specialized;

namespace Gstc.Collections.Observable.Notify {
    public abstract class NotifyCollection : 
        NotifyProperty, 
        INotifyCollectionChanged {
        
        #region INotifyCollectionChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (CollectionChanged == null) return;
            using (BlockReentrancy()) { CollectionChanged?.Invoke(this, e); }
        }

        //Reset
        protected void OnCollectionChangedReset() => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        protected void OnCollectionChangedAdd(object value, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));

        protected void OnCollectionChangedAddMany(IList valueList, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, valueList, index));

        protected void OnCollectionChangedRemove(object value, int index)
           => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value, index));

        protected void OnCollectionChangedMove(object value, int index, int oldIndex)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, value, index, oldIndex));

        protected void OnCollectionChangedReplace(object oldValue, object newValue, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newValue, oldValue, index));

        #endregion
    }
}
