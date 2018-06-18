using System.Collections.Generic;
using System.Collections.Specialized;

namespace Gstc.Collections.Observable.Interface {
    public interface IObservableCollection<TItem> : 
        ICollection<TItem>, 
        INotifyCollectionChanged {}
}
