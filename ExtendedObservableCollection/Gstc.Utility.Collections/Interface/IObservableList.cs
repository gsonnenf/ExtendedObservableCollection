using System.Collections.Generic;

namespace Gstc.Collections.Observable.Interface {
    public interface IObservableList<TItem> :
        IList<TItem>,
        IObservableCollection<TItem> 
         {
    }
}
