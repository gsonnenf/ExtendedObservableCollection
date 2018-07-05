using System.Collections.Generic;
using System.Collections.Specialized;
using Gstc.Collections.Observable.Notify;

namespace Gstc.Collections.Observable.Interface {
    public interface IObservableDictionaryCollection<TKey,TValue> : 
        IObservableCollection, 
        IObservableDictionary<TKey,TValue> {

    }
}
