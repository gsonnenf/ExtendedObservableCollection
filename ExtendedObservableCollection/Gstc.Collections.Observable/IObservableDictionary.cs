using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable {
    public interface IObservableDictionary<TKey,TValue> :
        IDictionary,
        IDictionary<TKey, TValue>,       
        INotifyDictionaryChanged<TKey,TValue> {        
    }
}
