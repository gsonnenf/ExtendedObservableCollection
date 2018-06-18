using System;
using System.Collections.Generic;
using Gstc.Collections.Observable.Base;

namespace Gstc.Collections.Observable {
    public class ObservableDictionaryCollection<Tkey,TValue> : BaseDictionaryCollection<Tkey,TValue> {
        public ObservableDictionaryCollection() {
            throw new NotImplementedException();
        }

        //TODO: Add backing Collection and notifications.

        protected override ICollection<TValue> InternalCollection { get; }
        protected override IDictionary<Tkey, TValue> InternalDictionary => Dictionary;
        
        public Dictionary<Tkey,TValue> Dictionary { get; set; }
        public override TValue this[Tkey key] {
            get => Dictionary[key];
            set => Dictionary[key] = value;
        }

        public override void Add(Tkey key, TValue value) {
            Dictionary.Add(key, value);
        }

        public override bool Remove(Tkey key) {
            return Dictionary.Remove(key);
        }

        public override void Clear() {
            Dictionary.Clear();
        }
    }
}
