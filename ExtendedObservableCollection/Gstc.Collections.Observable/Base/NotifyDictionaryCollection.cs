﻿namespace Gstc.Collections.Observable.Base {
    public abstract class NotifyDictionaryCollection<TKey,TValue> : 
        NotifyCollection, 
        INotifyDictionaryChanged<TKey, TValue>
    {

        public event NotifyDictionaryChangedEventHandler<TKey, TValue> DictionaryChanged;

        protected virtual void OnDictionaryChanged(NotifyDictionaryChangedEventArgs<TKey, TValue> e) {
            if (DictionaryChanged == null) return;
            using (BlockReentrancy()) { DictionaryChanged?.Invoke(this, e); }
        }

        //Reset
        protected void OnDictionaryReset()
            => OnDictionaryChanged(new NotifyDictionaryChangedEventArgs<TKey, TValue>(NotifyDictionaryChangedAction.Reset));

        //Add / Remove
        protected void OnDictionaryChangedAdd(TKey key, TValue item)
            => OnDictionaryChanged(new NotifyDictionaryChangedEventArgs<TKey, TValue>(NotifyDictionaryChangedAction.Add, key, item));

        protected void OnDictionaryRemove(TKey key, TValue item)
            => OnDictionaryChanged(new NotifyDictionaryChangedEventArgs<TKey, TValue>(NotifyDictionaryChangedAction.Remove, key, item));

        //Replace
        protected void OnDictionaryReplace(TKey key, TValue oldItem, TValue newItem)
            => OnDictionaryChanged(new NotifyDictionaryChangedEventArgs<TKey, TValue>(NotifyDictionaryChangedAction.Replace, key, oldItem, newItem));

    }
}

