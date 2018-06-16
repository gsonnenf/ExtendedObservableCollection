﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Collections.Observable.Base {
    public interface IObservableDictionaryCollection<TKey,TValue> :
        IDictionary<TKey, TValue>,
        INotifyDictionaryChanged<TKey, TValue>,
        INotifyCollectionChanged
         {
    }
}
