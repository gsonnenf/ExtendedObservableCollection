using System;
using System.Collections;
using System.Collections.Generic;

namespace Gstc.Collections.Observable.misc {

    /// <summary>
    /// Enumerator that exposes model item type (T1) as model view item type (T2). 
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    public class EnumeratorAdapter<T1,T2> : IEnumerator where T2  : class where T1 : class {

        private readonly IEnumerator _enumerator;
        public Func<T1, T2> Adapt;

        public EnumeratorAdapter(IEnumerator enumerator, Func<T1, T2> adapt) {
            _enumerator = enumerator;
            Adapt = adapt;
        }       
        public T2 Current => Adapt?.Invoke(_enumerator.Current as T1);
        public bool MoveNext() => _enumerator.MoveNext();
        public void Reset() => _enumerator.Reset();
        object IEnumerator.Current => Current;
    }

    /// <summary>
    /// Enumerator that exposes model item type (T1) as model view item type (T2). 
    /// </summary>
    /// <typeparam name="T1">Input Type</typeparam>
    /// <typeparam name="T2">Output type</typeparam>
    public class EnumeratorAdapterTyped<T1, T2> : IEnumerator<T2> where T1 : class where T2 : class {

        private readonly IEnumerator<T1> _enumerator;
        public Func<T1, T2> Adapt;

        public EnumeratorAdapterTyped(IEnumerator<T1> enumerator, Func<T1, T2> adapt) {
            _enumerator = enumerator;
            Adapt = adapt;
        }
        public T2 Current => Adapt?.Invoke(_enumerator.Current as T1);
        public bool MoveNext() => _enumerator.MoveNext();
        public void Reset() => _enumerator.Reset();
        object IEnumerator.Current => Current;
        public void Dispose() => _enumerator.Dispose();
    }
}

