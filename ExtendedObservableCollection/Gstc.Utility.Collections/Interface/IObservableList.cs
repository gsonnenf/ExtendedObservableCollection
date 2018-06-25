using System.Collections.Generic;

namespace Gstc.Collections.Observable.Interface {
    public interface IObservableList<TItem> :
        IObservableCollection<TItem>,
        IList<TItem>
        
         {
    }
}
