using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gstc.Collections.Observable.Test {
    class Test<TValue> : ICollection<TValue> {
        public IEnumerator<TValue> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(TValue item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(TValue item) {
            throw new NotImplementedException();
        }

        public void CopyTo(TValue[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(TValue item) {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
    }


    class ListTest : IList<object> {
        public IEnumerator<object> GetEnumerator() {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public void Add(object item) {
            throw new NotImplementedException();
        }

        public void Clear() {
            throw new NotImplementedException();
        }

        public bool Contains(object item) {
            throw new NotImplementedException();
        }

        public void CopyTo(object[] array, int arrayIndex) {
            throw new NotImplementedException();
        }

        public bool Remove(object item) {
            throw new NotImplementedException();
        }

        public int Count { get; }
        public bool IsReadOnly { get; }
        public int IndexOf(object item) {
            throw new NotImplementedException();
        }

        public void Insert(int index, object item) {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index) {
            throw new NotImplementedException();
        }

        public object this[int index] {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}
