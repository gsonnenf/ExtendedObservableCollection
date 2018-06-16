using System;
using System.Collections.Generic;

namespace Gstc.Collections.Observable {
    /// <summary>
    /// This class is an implementation of ObservableListKeyed that defines its GetKey method
    /// via anonymous function at runtime.
    ///
    /// See the ObservableListKeyed for more details. 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class ObservableListKeyedFunc<TKey,TValue> : ObservableListKeyed<TKey,TValue> {
        
        public Func<TValue, TKey> GetKeyFunc;

        public ObservableListKeyedFunc(Func<TValue, TKey> getKeyFunc) {  GetKeyFunc = getKeyFunc; }

        public ObservableListKeyedFunc(Func<TValue, TKey> getKeyFunc, List<TValue>  list) : base(list) {
            GetKeyFunc = getKeyFunc;
        }

        public override TKey GetKey(TValue item) => GetKeyFunc(item);
    }
}
