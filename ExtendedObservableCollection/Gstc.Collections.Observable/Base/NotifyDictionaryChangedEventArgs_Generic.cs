using System.Collections.Generic;

namespace Gstc.Collections.Observable.Base {
    public class NotifyDictionaryChangedEventArgs<TKey,TValue> : NotifyDictionaryChangedEventArgs {

        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action) : base(action) { }

        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, TKey key, TValue changedItem) : base(action, key, changedItem) { }

        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, IList<TKey> keys, IList<TValue> changedItems) : base(action, keys, changedItems) {}

        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, TKey key, TValue oldItem, TValue newItem) : base(action, key, oldItem, newItem) {}

        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, IList<TKey> keys, IList<TValue> newItems, IList<TValue> oldItems) : base(action, keys, newItems, oldItems) {}


        //public new IList<TValue> NewItems => (IList<TValue>) base.NewItems;

        //public new IList<TValue> OldItems => (IList<TValue>) base.OldItems;

        //public new IList<TKey> NewKeys => (IList<TKey>) base.NewKeys;

        //public new IList<TKey> OldKeys => (IList<TKey>) base.OldKeys;

    }

    public interface INotifyDictionaryChanged<TKey, TValue> {
        event NotifyDictionaryChangedEventHandler<TKey, TValue> DictionaryChanged;
    }

    public delegate void NotifyDictionaryChangedEventHandler<TKey, TValue> (object sender, NotifyDictionaryChangedEventArgs<TKey, TValue> e);
}

   

