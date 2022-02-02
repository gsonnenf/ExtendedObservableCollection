using System.Collections;
using System.Collections.Generic;

namespace Gstc.Collections.Observable {

    /// <summary>
    /// A List that triggers INotifyCollectionChanged and INotifyPropertyChanged when list changes.
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public interface IObservableList<TItem> :
        IObservableCollection<TItem>,
        IList<TItem>,
        IList {

    }
}
