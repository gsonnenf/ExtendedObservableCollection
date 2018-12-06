// Decompiled with JetBrains decompiler
// Type: System.Collections.Generic.Dictionary`2
// Assembly: mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089
// MVID: 363257E3-9D24-4888-B16F-C2A2FFEC12DF
// Assembly location: C:\Windows\Microsoft.NET\Framework\v4.0.30319\mscorlib.dll

/*
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using System.Threading;

namespace System.Collections.Generic {
      
    [Serializable]
    public class Dictionary3<TKey, TValue> : IDictionary<TKey, TValue>, IDictionary, IReadOnlyDictionary<TKey, TValue>, ISerializable, IDeserializationCallback {
        private int[] buckets;
        private Entry[] entries;
        private int count;
        private int version;
        private int freeList;
        private int freeCount;
        private IEqualityComparer<TKey> comparer;
        private KeyCollection keys;
        private ValueCollection values;
        private object _syncRoot;
        private const string VersionName = "Version";
        private const string HashSizeName = "HashSize";
        private const string KeyValuePairsName = "KeyValuePairs";
        private const string ComparerName = "Comparer";
        
        public IEqualityComparer<TKey> Comparer => this.comparer;

        public int Count => this.count - this.freeCount;

        public KeyCollection Keys => this.keys ?? (this.keys = new KeyCollection(this));

        ICollection<TKey> IDictionary<TKey, TValue>.Keys {
            
            get {
                return (keys != null) ? keys : new KeyCollection(this); 
                if (keys != null) return keys;
                keys = new KeyCollection(this);
                return keys;
            }
        }

        
        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys {
            
            get {
                if (this.keys == null)
                    this.keys = new Dictionary<TKey, TValue>.KeyCollection(this);
                return (IEnumerable<TKey>)this.keys;
            }
        }

        
        public Dictionary<TKey, TValue>.ValueCollection Values {
            
            get {
                if (this.values == null)
                    this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
                return this.values;
            }
        }

        
        ICollection<TValue> IDictionary<TKey, TValue>.Values {
            
            get {
                if (this.values == null)
                    this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
                return (ICollection<TValue>)this.values;
            }
        }

        
        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values {
            
            get {
                if (this.values == null)
                    this.values = new Dictionary<TKey, TValue>.ValueCollection(this);
                return (IEnumerable<TValue>)this.values;
            }
        }

        
        public TValue this[TKey key] {
            
            get {
                int entry = this.FindEntry(key);
                if (entry >= 0)
                    return this.entries[entry].value;
                ThrowHelper.ThrowKeyNotFoundException();
                return default(TValue);
            }
            
            set {
                this.Insert(key, value, false);
            }
        }

        
        bool ICollection<KeyValuePair<TKey, TValue>>.IsReadOnly {
            
            get {
                return false;
            }
        }

        
        bool ICollection.IsSynchronized {
            
            get {
                return false;
            }
        }

        
        object ICollection.SyncRoot {
            
            get {
                if (this._syncRoot == null)
                    Interlocked.CompareExchange<object>(ref this._syncRoot, new object(), (object)null);
                return this._syncRoot;
            }
        }

        
        bool IDictionary.IsFixedSize {
            
            get {
                return false;
            }
        }

        
        bool IDictionary.IsReadOnly {
            
            get {
                return false;
            }
        }

        
        ICollection IDictionary.Keys {
            
            get {
                return (ICollection)this.Keys;
            }
        }

        
        ICollection IDictionary.Values {
            
            get {
                return (ICollection)this.Values;
            }
        }

        
        object IDictionary.this[object key] {
            
            get {
                if (Dictionary<TKey, TValue>.IsCompatibleKey(key)) {
                    int entry = this.FindEntry((TKey)key);
                    if (entry >= 0)
                        return (object)this.entries[entry].value;
                }
                return (object)null;
            }
            
            set {
                if (key == null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
                ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
                try {
                    TKey index = (TKey)key;
                    try {
                        this[index] = (TValue)value;
                    } catch (InvalidCastException ex) {
                        ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                    }
                } catch (InvalidCastException ex) {
                    ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
                }
            }
        }

        
        public Dictionary()
          : this(0, (IEqualityComparer<TKey>)null) {
        }

        
        public Dictionary(int capacity)
          : this(capacity, (IEqualityComparer<TKey>)null) {
        }

        
        public Dictionary(IEqualityComparer<TKey> comparer)
          : this(0, comparer) {
        }

        
        public Dictionary(int capacity, IEqualityComparer<TKey> comparer) {
            if (capacity < 0)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.capacity);
            if (capacity > 0)
                this.Initialize(capacity);
            this.comparer = comparer ?? (IEqualityComparer<TKey>)EqualityComparer<TKey>.Default;
        }

        
        public Dictionary(IDictionary<TKey, TValue> dictionary)
          : this(dictionary, (IEqualityComparer<TKey>)null) {
        }

        
        public Dictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
          : this(dictionary != null ? dictionary.Count : 0, comparer) {
            if (dictionary == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
            foreach (KeyValuePair<TKey, TValue> keyValuePair in (IEnumerable<KeyValuePair<TKey, TValue>>)dictionary)
                this.Add(keyValuePair.Key, keyValuePair.Value);
        }

        protected Dictionary(SerializationInfo info, StreamingContext context) {
            HashHelpers.SerializationInfoTable.Add((object)this, info);
        }

        
        public void Add(TKey key, TValue value) {
            this.Insert(key, value, true);
        }

        
        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> keyValuePair) {
            this.Add(keyValuePair.Key, keyValuePair.Value);
        }

        
        bool ICollection<KeyValuePair<TKey, TValue>>.Contains(KeyValuePair<TKey, TValue> keyValuePair) {
            int entry = this.FindEntry(keyValuePair.Key);
            return entry >= 0 && EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value);
        }

        
        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> keyValuePair) {
            int entry = this.FindEntry(keyValuePair.Key);
            if (entry < 0 || !EqualityComparer<TValue>.Default.Equals(this.entries[entry].value, keyValuePair.Value))
                return false;
            this.Remove(keyValuePair.Key);
            return true;
        }

        
        public void Clear() {
            if (this.count <= 0)
                return;
            for (int index = 0; index < this.buckets.Length; ++index)
                this.buckets[index] = -1;
            Array.Clear((Array)this.entries, 0, this.count);
            this.freeList = -1;
            this.count = 0;
            this.freeCount = 0;
            this.version = this.version + 1;
        }

        
        public bool ContainsKey(TKey key) {
            return this.FindEntry(key) >= 0;
        }

        
        public bool ContainsValue(TValue value) {
            if ((object)value == null) {
                for (int index = 0; index < this.count; ++index) {
                    if (this.entries[index].hashCode >= 0 && (object)this.entries[index].value == null)
                        return true;
                }
            } else {
                EqualityComparer<TValue> equalityComparer = EqualityComparer<TValue>.Default;
                for (int index = 0; index < this.count; ++index) {
                    if (this.entries[index].hashCode >= 0 && equalityComparer.Equals(this.entries[index].value, value))
                        return true;
                }
            }
            return false;
        }

        private void CopyTo(KeyValuePair<TKey, TValue>[] array, int index) {
            if (array == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            if (index < 0 || index > array.Length)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            if (array.Length - index < this.Count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            int count = this.count;
            Dictionary<TKey, TValue>.Entry[] entries = this.entries;
            for (int index1 = 0; index1 < count; ++index1) {
                if (entries[index1].hashCode >= 0)
                    array[index++] = new KeyValuePair<TKey, TValue>(entries[index1].key, entries[index1].value);
            }
        }

        
        public Dictionary<TKey, TValue>.Enumerator GetEnumerator() {
            return new Dictionary<TKey, TValue>.Enumerator(this, 2);
        }

        
        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator() {
            return (IEnumerator<KeyValuePair<TKey, TValue>>)new Dictionary<TKey, TValue>.Enumerator(this, 2);
        }

        [SecurityCritical]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context) {
            if (info == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.info);
            info.AddValue("Version", this.version);
            info.AddValue("Comparer", HashHelpers.GetEqualityComparerForSerialization((object)this.comparer), typeof(IEqualityComparer<TKey>));
            info.AddValue("HashSize", this.buckets == null ? 0 : this.buckets.Length);
            if (this.buckets == null)
                return;
            KeyValuePair<TKey, TValue>[] array = new KeyValuePair<TKey, TValue>[this.Count];
            this.CopyTo(array, 0);
            info.AddValue("KeyValuePairs", (object)array, typeof(KeyValuePair<TKey, TValue>[]));
        }

        private int FindEntry(TKey key) {
            if ((object)key == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            if (this.buckets != null) {
                int num = this.comparer.GetHashCode(key) & int.MaxValue;
                for (int index = this.buckets[num % this.buckets.Length]; index >= 0; index = this.entries[index].next) {
                    if (this.entries[index].hashCode == num && this.comparer.Equals(this.entries[index].key, key))
                        return index;
                }
            }
            return -1;
        }

        private void Initialize(int capacity) {
            int prime = HashHelpers.GetPrime(capacity);
            this.buckets = new int[prime];
            for (int index = 0; index < this.buckets.Length; ++index)
                this.buckets[index] = -1;
            this.entries = new Dictionary<TKey, TValue>.Entry[prime];
            this.freeList = -1;
        }

        private void Insert(TKey key, TValue value, bool add) {
            if ((object)key == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            if (this.buckets == null)
                this.Initialize(0);
            int num1 = this.comparer.GetHashCode(key) & int.MaxValue;
            int index1 = num1 % this.buckets.Length;
            int num2 = 0;
            for (int index2 = this.buckets[index1]; index2 >= 0; index2 = this.entries[index2].next) {
                if (this.entries[index2].hashCode == num1 && this.comparer.Equals(this.entries[index2].key, key)) {
                    if (add)
                        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_AddingDuplicate);
                    this.entries[index2].value = value;
                    this.version = this.version + 1;
                    return;
                }
                ++num2;
            }
            int index3;
            if (this.freeCount > 0) {
                index3 = this.freeList;
                this.freeList = this.entries[index3].next;
                this.freeCount = this.freeCount - 1;
            } else {
                if (this.count == this.entries.Length) {
                    this.Resize();
                    index1 = num1 % this.buckets.Length;
                }
                index3 = this.count;
                this.count = this.count + 1;
            }
            this.entries[index3].hashCode = num1;
            this.entries[index3].next = this.buckets[index1];
            this.entries[index3].key = key;
            this.entries[index3].value = value;
            this.buckets[index1] = index3;
            this.version = this.version + 1;
            if (num2 <= 100 || !HashHelpers.IsWellKnownEqualityComparer((object)this.comparer))
                return;
            this.comparer = (IEqualityComparer<TKey>)HashHelpers.GetRandomizedEqualityComparer((object)this.comparer);
            this.Resize(this.entries.Length, true);
        }

        public virtual void OnDeserialization(object sender) {
            SerializationInfo serializationInfo;
            HashHelpers.SerializationInfoTable.TryGetValue((object)this, out serializationInfo);
            if (serializationInfo == null)
                return;
            int int32_1 = serializationInfo.GetInt32("Version");
            int int32_2 = serializationInfo.GetInt32("HashSize");
            this.comparer = (IEqualityComparer<TKey>)serializationInfo.GetValue("Comparer", typeof(IEqualityComparer<TKey>));
            if (int32_2 != 0) {
                this.buckets = new int[int32_2];
                for (int index = 0; index < this.buckets.Length; ++index)
                    this.buckets[index] = -1;
                this.entries = new Dictionary<TKey, TValue>.Entry[int32_2];
                this.freeList = -1;
                KeyValuePair<TKey, TValue>[] keyValuePairArray = (KeyValuePair<TKey, TValue>[])serializationInfo.GetValue("KeyValuePairs", typeof(KeyValuePair<TKey, TValue>[]));
                if (keyValuePairArray == null)
                    ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_MissingKeys);
                for (int index = 0; index < keyValuePairArray.Length; ++index) {
                    if ((object)keyValuePairArray[index].Key == null)
                        ThrowHelper.ThrowSerializationException(ExceptionResource.Serialization_NullKey);
                    this.Insert(keyValuePairArray[index].Key, keyValuePairArray[index].Value, true);
                }
            } else
                this.buckets = (int[])null;
            this.version = int32_1;
            HashHelpers.SerializationInfoTable.Remove((object)this);
        }

        private void Resize() {
            this.Resize(HashHelpers.ExpandPrime(this.count), false);
        }

        private void Resize(int newSize, bool forceNewHashCodes) {
            int[] numArray = new int[newSize];
            for (int index = 0; index < numArray.Length; ++index)
                numArray[index] = -1;
            Dictionary<TKey, TValue>.Entry[] entryArray = new Dictionary<TKey, TValue>.Entry[newSize];
            Array.Copy((Array)this.entries, 0, (Array)entryArray, 0, this.count);
            if (forceNewHashCodes) {
                for (int index = 0; index < this.count; ++index) {
                    if (entryArray[index].hashCode != -1)
                        entryArray[index].hashCode = this.comparer.GetHashCode(entryArray[index].key) & int.MaxValue;
                }
            }
            for (int index1 = 0; index1 < this.count; ++index1) {
                if (entryArray[index1].hashCode >= 0) {
                    int index2 = entryArray[index1].hashCode % newSize;
                    entryArray[index1].next = numArray[index2];
                    numArray[index2] = index1;
                }
            }
            this.buckets = numArray;
            this.entries = entryArray;
        }

        
        public bool Remove(TKey key) {
            if ((object)key == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            if (this.buckets != null) {
                int num = this.comparer.GetHashCode(key) & int.MaxValue;
                int index1 = num % this.buckets.Length;
                int index2 = -1;
                for (int index3 = this.buckets[index1]; index3 >= 0; index3 = this.entries[index3].next) {
                    if (this.entries[index3].hashCode == num && this.comparer.Equals(this.entries[index3].key, key)) {
                        if (index2 < 0)
                            this.buckets[index1] = this.entries[index3].next;
                        else
                            this.entries[index2].next = this.entries[index3].next;
                        this.entries[index3].hashCode = -1;
                        this.entries[index3].next = this.freeList;
                        this.entries[index3].key = default(TKey);
                        this.entries[index3].value = default(TValue);
                        this.freeList = index3;
                        this.freeCount = this.freeCount + 1;
                        this.version = this.version + 1;
                        return true;
                    }
                    index2 = index3;
                }
            }
            return false;
        }

        
        public bool TryGetValue(TKey key, out TValue value) {
            int entry = this.FindEntry(key);
            if (entry >= 0) {
                value = this.entries[entry].value;
                return true;
            }
            value = default(TValue);
            return false;
        }

        internal TValue GetValueOrDefault(TKey key) {
            int entry = this.FindEntry(key);
            if (entry >= 0)
                return this.entries[entry].value;
            return default(TValue);
        }

        
        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int index) {
            this.CopyTo(array, index);
        }

        
        void ICollection.CopyTo(Array array, int index) {
            if (array == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
            if (array.Rank != 1)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
            if (array.GetLowerBound(0) != 0)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
            if (index < 0 || index > array.Length)
                ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
            if (array.Length - index < this.Count)
                ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
            KeyValuePair<TKey, TValue>[] array1 = array as KeyValuePair<TKey, TValue>[];
            if (array1 != null)
                this.CopyTo(array1, index);
            else if (array is DictionaryEntry[]) {
                DictionaryEntry[] dictionaryEntryArray = array as DictionaryEntry[];
                Dictionary<TKey, TValue>.Entry[] entries = this.entries;
                for (int index1 = 0; index1 < this.count; ++index1) {
                    if (entries[index1].hashCode >= 0)
                        dictionaryEntryArray[index++] = new DictionaryEntry((object)entries[index1].key, (object)entries[index1].value);
                }
            } else {
                object[] objArray = array as object[];
                if (objArray == null)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                try {
                    int count = this.count;
                    Dictionary<TKey, TValue>.Entry[] entries = this.entries;
                    for (int index1 = 0; index1 < count; ++index1) {
                        if (entries[index1].hashCode >= 0)
                            objArray[index++] = (object)new KeyValuePair<TKey, TValue>(entries[index1].key, entries[index1].value);
                    }
                } catch (ArrayTypeMismatchException ex) {
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                }
            }
        }

        
        IEnumerator IEnumerable.GetEnumerator() {
            return (IEnumerator)new Dictionary<TKey, TValue>.Enumerator(this, 2);
        }

        private static bool IsCompatibleKey(object key) {
            if (key == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            return key is TKey;
        }

        
        void IDictionary.Add(object key, object value) {
            if (key == null)
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.key);
            ThrowHelper.IfNullAndNullsAreIllegalThenThrow<TValue>(value, ExceptionArgument.value);
            try {
                TKey key1 = (TKey)key;
                try {
                    this.Add(key1, (TValue)value);
                } catch (InvalidCastException ex) {
                    ThrowHelper.ThrowWrongValueTypeArgumentException(value, typeof(TValue));
                }
            } catch (InvalidCastException ex) {
                ThrowHelper.ThrowWrongKeyTypeArgumentException(key, typeof(TKey));
            }
        }

        
        bool IDictionary.Contains(object key) {
            if (Dictionary<TKey, TValue>.IsCompatibleKey(key))
                return this.ContainsKey((TKey)key);
            return false;
        }

        
        IDictionaryEnumerator IDictionary.GetEnumerator() {
            return (IDictionaryEnumerator)new Dictionary<TKey, TValue>.Enumerator(this, 1);
        }

        
        void IDictionary.Remove(object key) {
            if (!Dictionary<TKey, TValue>.IsCompatibleKey(key))
                return;
            this.Remove((TKey)key);
        }

        private struct Entry {
            public int hashCode;
            public int next;
            public TKey key;
            public TValue value;
        }

        
        [Serializable]
        public struct Enumerator : IEnumerator<KeyValuePair<TKey, TValue>>, IDisposable, IEnumerator, IDictionaryEnumerator {
            private Dictionary<TKey, TValue> dictionary;
            private int version;
            private int index;
            private KeyValuePair<TKey, TValue> current;
            private int getEnumeratorRetType;
            internal const int DictEntry = 1;
            internal const int KeyValuePair = 2;

            
            public KeyValuePair<TKey, TValue> Current {
                
                get {
                    return this.current;
                }
            }

            
            object IEnumerator.Current {
                
                get {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    if (this.getEnumeratorRetType == 1)
                        return (object)new DictionaryEntry((object)this.current.Key, (object)this.current.Value);
                    return (object)new KeyValuePair<TKey, TValue>(this.current.Key, this.current.Value);
                }
            }

            
            DictionaryEntry IDictionaryEnumerator.Entry {
                
                get {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    return new DictionaryEntry((object)this.current.Key, (object)this.current.Value);
                }
            }

            
            object IDictionaryEnumerator.Key {
                
                get {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    return (object)this.current.Key;
                }
            }

            
            object IDictionaryEnumerator.Value {
                
                get {
                    if (this.index == 0 || this.index == this.dictionary.count + 1)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                    return (object)this.current.Value;
                }
            }

            internal Enumerator(Dictionary<TKey, TValue> dictionary, int getEnumeratorRetType) {
                this.dictionary = dictionary;
                this.version = dictionary.version;
                this.index = 0;
                this.getEnumeratorRetType = getEnumeratorRetType;
                this.current = new KeyValuePair<TKey, TValue>();
            }

            
            public bool MoveNext() {
                if (this.version != this.dictionary.version)
                    ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                for (; (uint)this.index < (uint)this.dictionary.count; this.index = this.index + 1) {
                    if (this.dictionary.entries[this.index].hashCode >= 0) {
                        this.current = new KeyValuePair<TKey, TValue>(this.dictionary.entries[this.index].key, this.dictionary.entries[this.index].value);
                        this.index = this.index + 1;
                        return true;
                    }
                }
                this.index = this.dictionary.count + 1;
                this.current = new KeyValuePair<TKey, TValue>();
                return false;
            }

            
            public void Dispose() {
            }

            
            void IEnumerator.Reset() {
                if (this.version != this.dictionary.version)
                    ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                this.index = 0;
                this.current = new KeyValuePair<TKey, TValue>();
            }
        }

        [DebuggerTypeProxy(typeof(Mscorlib_DictionaryKeyCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        
        [Serializable]
        public sealed class KeyCollection : ICollection<TKey>, IEnumerable<TKey>, IEnumerable, ICollection, IReadOnlyCollection<TKey> {
            private Dictionary<TKey, TValue> dictionary;

            
            public int Count {
                
                get {
                    return this.dictionary.Count;
                }
            }

            
            bool ICollection<TKey>.IsReadOnly {
                
                get {
                    return true;
                }
            }

            
            bool ICollection.IsSynchronized {
                
                get {
                    return false;
                }
            }

            
            object ICollection.SyncRoot {
                
                get {
                    return ((ICollection)this.dictionary).SyncRoot;
                }
            }

            
            public KeyCollection(Dictionary<TKey, TValue> dictionary) {
                if (dictionary == null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
                this.dictionary = dictionary;
            }

            
            public Dictionary<TKey, TValue>.KeyCollection.Enumerator GetEnumerator() {
                return new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
            }

            
            public void CopyTo(TKey[] array, int index) {
                if (array == null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                if (index < 0 || index > array.Length)
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                int count = this.dictionary.count;
                Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
                for (int index1 = 0; index1 < count; ++index1) {
                    if (entries[index1].hashCode >= 0)
                        array[index++] = entries[index1].key;
                }
            }

            
            void ICollection<TKey>.Add(TKey item) {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
            }

            
            void ICollection<TKey>.Clear() {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
            }

            
            bool ICollection<TKey>.Contains(TKey item) {
                return this.dictionary.ContainsKey(item);
            }

            
            bool ICollection<TKey>.Remove(TKey item) {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_KeyCollectionSet);
                return false;
            }

            
            IEnumerator<TKey> IEnumerable<TKey>.GetEnumerator() {
                return (IEnumerator<TKey>)new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
            }

            
            IEnumerator IEnumerable.GetEnumerator() {
                return (IEnumerator)new Dictionary<TKey, TValue>.KeyCollection.Enumerator(this.dictionary);
            }

            
            void ICollection.CopyTo(Array array, int index) {
                if (array == null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                if (array.Rank != 1)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                if (array.GetLowerBound(0) != 0)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                if (index < 0 || index > array.Length)
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                TKey[] array1 = array as TKey[];
                if (array1 != null) {
                    this.CopyTo(array1, index);
                } else {
                    object[] objArray = array as object[];
                    if (objArray == null)
                        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                    int count = this.dictionary.count;
                    Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
                    try {
                        for (int index1 = 0; index1 < count; ++index1) {
                            if (entries[index1].hashCode >= 0)
                                objArray[index++] = (object)entries[index1].key;
                        }
                    } catch (ArrayTypeMismatchException ex) {
                        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                    }
                }
            }

            
            [Serializable]
            public struct Enumerator : IEnumerator<TKey>, IDisposable, IEnumerator {
                private Dictionary<TKey, TValue> dictionary;
                private int index;
                private int version;
                private TKey currentKey;

                
                public TKey Current {
                    
                    get {
                        return this.currentKey;
                    }
                }

                
                object IEnumerator.Current {
                    
                    get {
                        if (this.index == 0 || this.index == this.dictionary.count + 1)
                            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                        return (object)this.currentKey;
                    }
                }

                internal Enumerator(Dictionary<TKey, TValue> dictionary) {
                    this.dictionary = dictionary;
                    this.version = dictionary.version;
                    this.index = 0;
                    this.currentKey = default(TKey);
                }

                
                public void Dispose() {
                }

                
                public bool MoveNext() {
                    if (this.version != this.dictionary.version)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    for (; (uint)this.index < (uint)this.dictionary.count; this.index = this.index + 1) {
                        if (this.dictionary.entries[this.index].hashCode >= 0) {
                            this.currentKey = this.dictionary.entries[this.index].key;
                            this.index = this.index + 1;
                            return true;
                        }
                    }
                    this.index = this.dictionary.count + 1;
                    this.currentKey = default(TKey);
                    return false;
                }

                
                void IEnumerator.Reset() {
                    if (this.version != this.dictionary.version)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    this.index = 0;
                    this.currentKey = default(TKey);
                }
            }
        }

        [DebuggerTypeProxy(typeof(Mscorlib_DictionaryValueCollectionDebugView<,>))]
        [DebuggerDisplay("Count = {Count}")]
        
        [Serializable]
        public sealed class ValueCollection : ICollection<TValue>, IEnumerable<TValue>, IEnumerable, ICollection, IReadOnlyCollection<TValue> {
            private Dictionary<TKey, TValue> dictionary;

            
            public int Count {
                
                get {
                    return this.dictionary.Count;
                }
            }

            
            bool ICollection<TValue>.IsReadOnly {
                
                get {
                    return true;
                }
            }

            
            bool ICollection.IsSynchronized {
                
                get {
                    return false;
                }
            }

            
            object ICollection.SyncRoot {
                
                get {
                    return ((ICollection)this.dictionary).SyncRoot;
                }
            }

            
            public ValueCollection(Dictionary<TKey, TValue> dictionary) {
                if (dictionary == null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.dictionary);
                this.dictionary = dictionary;
            }

            
            public Dictionary<TKey, TValue>.ValueCollection.Enumerator GetEnumerator() {
                return new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
            }

            
            public void CopyTo(TValue[] array, int index) {
                if (array == null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                if (index < 0 || index > array.Length)
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                int count = this.dictionary.count;
                Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
                for (int index1 = 0; index1 < count; ++index1) {
                    if (entries[index1].hashCode >= 0)
                        array[index++] = entries[index1].value;
                }
            }

            
            void ICollection<TValue>.Add(TValue item) {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
            }

            
            bool ICollection<TValue>.Remove(TValue item) {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
                return false;
            }

            
            void ICollection<TValue>.Clear() {
                ThrowHelper.ThrowNotSupportedException(ExceptionResource.NotSupported_ValueCollectionSet);
            }

            
            bool ICollection<TValue>.Contains(TValue item) {
                return this.dictionary.ContainsValue(item);
            }

            
            IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() {
                return (IEnumerator<TValue>)new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
            }

            
            IEnumerator IEnumerable.GetEnumerator() {
                return (IEnumerator)new Dictionary<TKey, TValue>.ValueCollection.Enumerator(this.dictionary);
            }

            
            void ICollection.CopyTo(Array array, int index) {
                if (array == null)
                    ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
                if (array.Rank != 1)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_RankMultiDimNotSupported);
                if (array.GetLowerBound(0) != 0)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_NonZeroLowerBound);
                if (index < 0 || index > array.Length)
                    ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
                if (array.Length - index < this.dictionary.Count)
                    ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall);
                TValue[] array1 = array as TValue[];
                if (array1 != null) {
                    this.CopyTo(array1, index);
                } else {
                    object[] objArray = array as object[];
                    if (objArray == null)
                        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                    int count = this.dictionary.count;
                    Dictionary<TKey, TValue>.Entry[] entries = this.dictionary.entries;
                    try {
                        for (int index1 = 0; index1 < count; ++index1) {
                            if (entries[index1].hashCode >= 0)
                                objArray[index++] = (object)entries[index1].value;
                        }
                    } catch (ArrayTypeMismatchException ex) {
                        ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_InvalidArrayType);
                    }
                }
            }

            
            [Serializable]
            public struct Enumerator : IEnumerator<TValue>, IDisposable, IEnumerator {
                private Dictionary<TKey, TValue> dictionary;
                private int index;
                private int version;
                private TValue currentValue;

                
                public TValue Current {
                    
                    get {
                        return this.currentValue;
                    }
                }

                
                object IEnumerator.Current {
                    
                    get {
                        if (this.index == 0 || this.index == this.dictionary.count + 1)
                            ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumOpCantHappen);
                        return (object)this.currentValue;
                    }
                }

                internal Enumerator(Dictionary<TKey, TValue> dictionary) {
                    this.dictionary = dictionary;
                    this.version = dictionary.version;
                    this.index = 0;
                    this.currentValue = default(TValue);
                }

                
                public void Dispose() {
                }

                
                public bool MoveNext() {
                    if (this.version != this.dictionary.version)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    for (; (uint)this.index < (uint)this.dictionary.count; this.index = this.index + 1) {
                        if (this.dictionary.entries[this.index].hashCode >= 0) {
                            this.currentValue = this.dictionary.entries[this.index].value;
                            this.index = this.index + 1;
                            return true;
                        }
                    }
                    this.index = this.dictionary.count + 1;
                    this.currentValue = default(TValue);
                    return false;
                }

                
                void IEnumerator.Reset() {
                    if (this.version != this.dictionary.version)
                        ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_EnumFailedVersion);
                    this.index = 0;
                    this.currentValue = default(TValue);
                }
            }
        }
    }
}

*/