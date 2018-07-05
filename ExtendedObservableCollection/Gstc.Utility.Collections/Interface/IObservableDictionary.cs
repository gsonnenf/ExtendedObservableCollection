﻿using System.Collections;
using System.Collections.Generic;
using Gstc.Collections.Observable.Notify;

namespace Gstc.Collections.Observable.Interface {
    public interface IObservableDictionary<TKey,TValue> :
        IDictionary,
        IDictionary<TKey, TValue>,       
        INotifyDictionaryChanged<TKey,TValue> {        
    }
}
