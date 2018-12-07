using System.Collections.Generic;
using System.Collections.Specialized;

namespace Gstc.Collections.Observable {
    public interface IObservableDictionaryCollection<TKey,TValue> : 
        IObservableCollection, 
        IObservableDictionary<TKey,TValue> {

    }
}
