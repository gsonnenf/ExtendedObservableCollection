using Gstc.Collections.Observable.Base;
using System.Collections;
using System.Collections.Generic;

namespace Gstc.Collections.Observable {
    public interface IObservableDictionary<TKey, TValue> :
        IDictionary,
        IDictionary<TKey, TValue>,
        INotifyDictionaryChanged<TKey, TValue> {
    }
}
