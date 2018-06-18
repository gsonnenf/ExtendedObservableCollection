using System.Collections;
using System.Collections.Specialized;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable.Notify {
    public abstract class NotifyCollectionBase : NotifyBase, INotifyCollectionChanged {
        #region INotifyCollectionChanged
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e) {
            if (CollectionChanged == null) return;
            using (BlockReentrancy()) { CollectionChanged(this, e); }
        }

        //Reset
        protected void OnCollectionReset() => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));

        protected void OnCollectionAdd(object value, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, value, index));

        protected void OnCollectionAddEnumerable(IList valueList, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, valueList, index));

        protected void OnCollectionRemove(object value, int index)
           => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, value, index));

        protected void OnCollectionMove(object value, int index, int oldIndex)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, value, index, oldIndex));

        protected void OnCollectionReplace(object oldValue, object newValue, int index)
            => OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newValue, oldValue, index));

        #endregion
    }
}
