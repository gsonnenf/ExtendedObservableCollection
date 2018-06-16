using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable.Interface {
    public interface IObservableList<TItem> :
        IList<TItem>,
        IObservableCollection<TItem> 
         {
    }
}
