using System;
using System.Collections.Generic;
using System.Text;

namespace Gstc.Collections.Observable.Base {
    public class NotifyDictionaryChangedEventArgs {

        #region contructors
        /// <summary>
        /// Construct a NotifyDictionaryChangedEventArgs that describes a reset change.
        /// </summary>
        /// <param name="action">The action that caused the event; must be Reset action.</param>
        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action) {
            if (action != NotifyDictionaryChangedAction.Reset) throw new ArgumentException(SR.GetString(SR.WrongActionForCtor, NotifyDictionaryChangedAction.Reset), nameof(action));
            InitializeAdd(action, null, null);
        }

        /// <summary>
        /// Construct a NotifyDictionaryChangedEventArgs that describes a one-item change.
        /// </summary>
        /// <param name="action">The action that caused the event; must be Add or Remove action.</param>
        /// <param name="changedItem">The item affected by the change.</param>
        /// <param name="key">The key where the change occurred.</param>
        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, object key, object changedItem) {
            if (action == NotifyDictionaryChangedAction.Add) InitializeAdd(action, new[] { key }, new[] { changedItem });
            else if (action == NotifyDictionaryChangedAction.Remove) InitializeRemove(action, new[] { key }, new[] { changedItem });
            else throw new ArgumentException(SR.GetString(SR.MustBeResetAddOrRemoveActionForCtor), "action");
        }

        /// <summary>
        /// Construct a NotifyDictionaryChangedEventArgs that describes a multi-item change; can only be Add or Remove action.
        /// </summary>
        /// <param name="action">The action that caused the event.</param>
        /// <param name="changedItems">The items affected by the change.</param>
        /// <param name="keys">Keys of items that have been changed.</param>
        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, IList<object> keys, IList<object> changedItems) {

            if (changedItems == null) throw new ArgumentNullException("changedItems");
            if (keys == null || keys.Count == 0) throw new ArgumentException(SR.GetString(SR.KeyMustNotBeNullOrZeroLength), "Keys");

            if (action == NotifyDictionaryChangedAction.Add) InitializeAdd(action, keys, changedItems);
            else if (action == NotifyDictionaryChangedAction.Remove) InitializeRemove(action, keys, changedItems);
            else throw new ArgumentException(SR.GetString(SR.MustBeResetAddOrRemoveActionForCtor), "action");

        }


        /// <summary>
        /// Construct a NotifyDictionaryChangedEventArgs that describes a one-item Replace event, can only be Replace.
        /// </summary>
        /// <param name="action">Can only be a Replace action.</param>
        /// <param name="newItem">The new item replacing the original item.</param>
        /// <param name="oldItem">The original item that is replaced.</param>
        /// <param name="key">Keys of items that have been replaced.</param>
        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, object key, object oldItem, object newItem) {
            if (action != NotifyDictionaryChangedAction.Replace) throw new ArgumentException(SR.GetString(SR.WrongActionForCtor, NotifyDictionaryChangedAction.Replace), "action");
            InitializeReplace(action, new[] { key }, new[] { oldItem }, new[] { newItem });
        }

        /// <summary>
        /// Construct a NotifyDictionaryChangedEventArgs that describes a multi-item Replace event.
        /// </summary>
        /// <param name="action">Can only be a Replace action.</param>
        /// <param name="newItems">The new items replacing the original items.</param>
        /// <param name="oldItems">The original items that are replaced.</param>
        /// <param name="keys">Keys of items that have been replaced.</param>
        public NotifyDictionaryChangedEventArgs(NotifyDictionaryChangedAction action, IList<object> keys, IList<object> newItems, IList<object> oldItems) {
            if (action != NotifyDictionaryChangedAction.Replace) throw new ArgumentException(SR.GetString(SR.WrongActionForCtor, NotifyDictionaryChangedAction.Replace), "action");
            if (newItems == null) throw new ArgumentNullException("newItems");
            if (oldItems == null) throw new ArgumentNullException("oldItems");
            InitializeReplace(action, keys, oldItems, newItems);
        }


        #endregion
        private void InitializeAdd(NotifyDictionaryChangedAction action, IList<object> newKeys, IList<object> newItems) {
            Action = action;
            NewItems = newItems;
            NewKeys = newKeys;
        }

        private void InitializeRemove(NotifyDictionaryChangedAction action, IList<object> oldKeys, IList<object> oldItems) {
            Action = action;
            OldItems = oldItems;
            OldKeys = oldKeys;
        }

        private void InitializeReplace(NotifyDictionaryChangedAction action, IList<object> keys, IList<object> oldItems, IList<object> newItems) {
            Action = action;
            NewItems = newItems;
            NewKeys = keys;
            OldItems = oldItems;
            OldKeys = keys;
        }

        /*
      private void InitializeMoveOrReplace(NotifyDictionaryChangedAction action, IList newItems, IList oldItems, IList newKeys, IList oldKeys) {
          InitializeAdd(action, newItems, newKeys);
          InitializeRemove(action, oldItems, oldKeys);
      }
      */


        #region Public Properties

        /// <summary>
        /// The action that caused the event.
        /// </summary>
        public NotifyDictionaryChangedAction Action { get; private set; }

        /// <summary>
        /// The items affected by the change.
        /// </summary>
        public IList<object> NewItems { get; private set; }

        /// <summary>
        /// The old items affected by the change (for Replace events).
        /// </summary>
        public IList<object> OldItems { get; private set; }

        /// <summary>
        /// The index where the change occurred.
        /// </summary>
        public IList<object> NewKeys { get; private set; }

        /// <summary>
        /// The old index where the change occurred (for Move events).
        /// </summary>
        public IList<object> OldKeys { get; private set; }

        #endregion

    }


    public interface INotifyDictionaryChanged {
        event NotifyDictionaryChangedEventHandler DictionaryChanged;
    }

    public delegate void NotifyDictionaryChangedEventHandler(object sender, NotifyDictionaryChangedEventArgs e);

    public enum NotifyDictionaryChangedAction {
        Add,
        Remove,
        Replace,
        Reset,
        //Move,
    }

    /// <summary>
    /// Kludge to keep code consistent with the .NET core error handling signatures.
    /// </summary>
    static class SR {
        internal const string WrongActionForCtor = "WrongActionForCtor";
        internal const string MustBeResetAddOrRemoveActionForCtor = "MustBeResetAddOrRemoveActionForCtor";
        internal const string ResetActionRequiresNullItem = "ResetActionRequiresNullItem";
        internal const string ResetActionRequiresNullKey = "ResetActionRequiresNullKey";
        internal const string KeyMustNotBeNullOrZeroLength = "KeyMustNotBeNullOrZeroLength";

        public static string GetString(string name, params object[] args) {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(name);
            stringBuilder.Append(" : ");
            foreach (var arg in args) {
                stringBuilder.Append(arg);
                stringBuilder.Append(", ");
            }
            return name;
        }
    }
}