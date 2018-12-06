
using System;
using System.Collections;
using System.Collections.Generic;
using AutoFixture;
using Gstc.Collections.Observable.Extended;
using Gstc.Collections.Observable.Interface;
using Gstc.Collections.Observable.Notify;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {
    public class InterfaceTestCases : CollectionTestBase<string, TestItem> {

        /// <summary>
        /// Test the interface for ICollection to make sure these are implemented.
        /// The collection passed to this test MUST come populated with 3 items.
        /// </summary>
        /// <param name="collection"></param>
        public void CollectionTest(ICollection collection) {

            //Count Test
            Assert.AreEqual(3, collection.Count);

            //Syncroot Test
            Assert.IsNotNull(collection.SyncRoot);

            //isSyncronized Test
            Assert.AreEqual(collection.IsSynchronized, false);

            //IEnumerator test
            IEnumerator enumerator = collection.GetEnumerator();
            Assert.IsNotNull(enumerator);

            //CopyTo(,) test
            Array array = new object[3];
            collection.CopyTo(array, 0);

            foreach (var item in array) {
                enumerator.MoveNext();
                Assert.AreEqual(item, enumerator.Current);
                Assert.IsTrue(item != null);
            }
        }

        /// <summary>
        /// Tests methods specific to ICollection<>. 
        /// </summary>
        /// <param name="collection"></param>
        public void CollectionGenericTest(ICollection<TestItem> collection) {

            Assert.IsNotNull(collection as IObservableCollection);

            MockEvent.AddNotifiersCollectionAndProperty(collection as IObservableCollection);
            //Add and count
            collection.Add(Item1);
            Assert.AreEqual(1, collection.Count);
            MockEvent.AssertMockNotifiersCollection(2, 1);

            //Remove Test
            collection.Remove(Item1);
            Assert.AreEqual(0, collection.Count);
            MockEvent.AssertMockNotifiersCollection(2, 1);

            collection.Add(Item1);
            collection.Add(Item2);
            collection.Add(Item3);
            Assert.AreEqual(3, collection.Count);
            MockEvent.AssertMockNotifiersCollection(6, 3);

            collection.Clear();
            Assert.AreEqual(0, collection.Count);
            MockEvent.AssertMockNotifiersCollection(2, 1);

            //Contains Test
            collection.Add(Item1);
            Assert.IsTrue(collection.Contains(Item1));
            Assert.IsFalse(collection.Contains(Item2));

            //Syncroot Test
            Assert.IsFalse(collection.IsReadOnly);

            //IEnumerator/ EnumeratorGeneric test
            collection.Clear();
            collection.Add(Item1);
            collection.Add(Item2);
            collection.Add(Item3);
            Assert.AreEqual(3, collection.Count);

            IEnumerator enumerator = collection.GetEnumerator();
            Assert.IsNotNull(enumerator);
            enumerator.MoveNext();
            Assert.AreEqual(enumerator.Current, Item1);

            IEnumerator<TestItem> enumeratorGeneric = collection.GetEnumerator();
            Assert.IsNotNull(enumeratorGeneric);
            enumeratorGeneric.MoveNext();
            Assert.AreEqual(enumeratorGeneric.Current, Item1);

            //CopyTo test
            var array = new TestItem[3];
            collection.CopyTo(array, 0);
            Assert.AreEqual(array[0], Item1);
            Assert.AreEqual(array[1], Item2);
            Assert.AreEqual(array[2], Item3);

        }

        public void ListTest(IList<TestItem> list) {

            Assert.IsNotNull(list as IObservableCollection);

            MockEvent.AddNotifiersCollectionAndProperty(list as IObservableCollection);

            //Index Test
            list.Add(Item1);
            Assert.AreEqual(Item1, list[0]);
            MockEvent.AssertMockNotifiersCollection(2, 1);

            list[0] = Item2;
            Assert.AreEqual(Item2, list[0]);
            MockEvent.AssertMockNotifiersCollection(1, 1);

            //Index of test
            Assert.AreEqual(0, list.IndexOf(Item2));

            //Insert(,)
            list.Insert(0, Item3);
            Assert.AreEqual(Item3, list[0]);
            MockEvent.AssertMockNotifiersCollection(2, 1);


            //RemoveAt()
            list.RemoveAt(0);
            Assert.AreEqual(Item2, list[0]);
            MockEvent.AssertMockNotifiersCollection(2, 1);
        }

        public void DictionaryTest(IDictionary dictionary) {

            Assert.IsNotNull(dictionary as INotifyDictionaryChanged<string, TestItem>);

            MockEvent.AddNotifiersDictionary(dictionary as INotifyDictionaryChanged<string, TestItem>);

            //Special case: ObservableListKeyed must have key match mapped property in object.
            if (dictionary is ObservableListKeyed<string, TestItem>) {
                Key1 = Item1.Id;
                Key2 = Item2.Id;
                Key3 = Item3.Id;
                DefaultKey = DefaultValue.Id;
                UpdateKey = UpdateValue.Id;
            }

            //Add Test
            dictionary.Add(Key1, Item1);
            Assert.AreEqual(1, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Contains test
            Assert.IsTrue(dictionary.Contains(Key1));
            Assert.IsFalse(dictionary.Contains(Key2));

            //Remove Test
            dictionary.Remove(Key1);
            Assert.AreEqual(0, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //index[] test
            dictionary[DefaultKey] = DefaultValue;
            Assert.AreEqual(1, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Special case: ObservableListKeyed must have key match mapped property in object.
            if (dictionary is ObservableListKeyed<string, TestItem>) UpdateValue.Id = DefaultKey;

            dictionary[DefaultKey] = UpdateValue;
            Assert.AreEqual(1, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Clear Test
            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Keys/Value Test

            //Special Case: Observable Sorted List will sort these, they must be in order or it will fail general test.
            if (dictionary is ObservableSortedList<string, TestItem>) {
                Key1 = "1";
                Key2 = "2";
            }

            dictionary.Add(Key1, Item1);
            dictionary.Add(Key2, Item2);

            var keys = dictionary.Keys.GetEnumerator();
            keys.MoveNext();
            Assert.AreSame(Key1, keys.Current);
            keys.MoveNext();
            Assert.AreSame(Key2, keys.Current);
          

            var values = dictionary.Values.GetEnumerator();
            values.MoveNext();
            Assert.AreSame(Item1, values.Current);
            values.MoveNext();
            Assert.AreSame(Item2, values.Current);
            
            //Readonlytest
            Assert.IsNotNull(dictionary.IsReadOnly);

            //IsFixedSize
            Assert.IsNotNull(dictionary.IsFixedSize);

            //Icollection
            var enumerator = dictionary.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreSame(Key1, enumerator.Key);
            Assert.AreSame(Item1, enumerator.Value);

            enumerator.MoveNext();
            Assert.AreSame(Key2, enumerator.Key);
            Assert.AreSame(Item2, enumerator.Value);
        }

        public void DictionaryGenericTest(IDictionary<string, TestItem> dictionary) {

            Assert.IsNotNull(dictionary as INotifyDictionaryChanged<string, TestItem>);
            MockEvent.AddNotifiersDictionary(dictionary as INotifyDictionaryChanged<string, TestItem>);

            //Special case: ObservableListKeyed must have key match mapped property in object.
            if (dictionary is ObservableListKeyed<string, TestItem>) {
                Key1 = Item1.Id;
                Key2 = Item2.Id;
                Key3 = Item3.Id;
                DefaultKey = DefaultValue.Id;
                UpdateKey = UpdateValue.Id;
            }

            //Add Test
            dictionary.Add(Key1, Item1);
            Assert.AreEqual(1, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Contains test
            Assert.IsTrue(dictionary.ContainsKey(Key1));
            Assert.IsFalse(dictionary.ContainsKey(Key2));

            //Remove Test
            dictionary.Remove(Key1);
            Assert.AreEqual(0, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //index[] test
            dictionary[DefaultKey] = DefaultValue;
            Assert.AreEqual(1, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Special case: ObservableListKeyed must have key match mapped property in object.
            if (dictionary is ObservableListKeyed<string, TestItem>) UpdateValue.Id = DefaultKey;

            dictionary[DefaultKey] = UpdateValue;
            Assert.AreEqual(1, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Clear Test
            dictionary.Clear();
            Assert.AreEqual(0, dictionary.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Keys/Value Test
            if (dictionary is ObservableSortedList<string, TestItem>) { //Special case for a sorted list
                Key1 = "1";
                Key2 = "2";
            }

            dictionary.Add(Key1, Item1);
            dictionary.Add(Key2, Item2);

            // ReSharper disable once GenericEnumeratorNotDisposed
            var keys = dictionary.Keys.GetEnumerator();
            keys.MoveNext();
            Assert.AreSame(Key1, keys.Current);
            keys.MoveNext();
            Assert.AreSame(Key2, keys.Current);
            keys.MoveNext();
            Assert.IsNull(keys.Current);

            // ReSharper disable once GenericEnumeratorNotDisposed
            var values = dictionary.Values.GetEnumerator();
            values.MoveNext();
            Assert.AreSame(Item1, values.Current);
            values.MoveNext();
            Assert.AreSame(Item2, values.Current);
            values.MoveNext();
            Assert.IsNull(values.Current);

            //TryGetValue test

            TestItem item = null;
            Assert.IsTrue(dictionary.TryGetValue(Key1,out item));
            Assert.IsFalse(dictionary.TryGetValue(Key3, out item));

            //Readonlytest
            Assert.IsNotNull(dictionary.IsReadOnly);

            var enumerator = dictionary.GetEnumerator();
            enumerator.MoveNext();
            Assert.AreSame(Key1, enumerator.Current.Key);
            Assert.AreSame(Item1, enumerator.Current.Value);

            enumerator.MoveNext();
            Assert.AreSame(Key2, enumerator.Current.Key);
            Assert.AreSame(Item2, enumerator.Current.Value);

            enumerator.MoveNext();
            Assert.IsNull(enumerator.Current.Key);
            Assert.IsNull(enumerator.Current.Value);

            //ICollection<KVP>, IEnumerable<KVP>, IEnumerable
               
            //TrygetValue
        }

        public void CollectionKeyValuePairTest(ICollection<KeyValuePair<string,TestItem>> collection) {

            Assert.IsNotNull(collection as INotifyDictionaryChanged<string,TestItem>);
            MockEvent.AddNotifiersDictionary(collection as INotifyDictionaryChanged<string, TestItem>);

            //Special case, ObservableListKeyed must have Keys match Item ID
            if (collection is ObservableListKeyed<string, TestItem>) {
                Key1 = Item1.Id;
                Key2 = Item2.Id;
                Key3 = Item3.Id;
            }

            //Special case, ObservableSortedList puts keys in order, so it must be added in alphabetical order to pass this general dictionary test.
            if (collection is ObservableSortedList<string, TestItem>) {
                Key1 = "1";
                Key2 = "2";
                Key3 = "3";
            }

            //Add and count
            var kvp1 = new KeyValuePair<string, TestItem>(Key1, Item1);
            var kvp2 = new KeyValuePair<string, TestItem>(Key2, Item2);
            var kvp3 = new KeyValuePair<string, TestItem>(Key3, Item3);

            collection.Add(kvp1);
            Assert.AreEqual(1, collection.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Remove Test
            collection.Remove(kvp1);
            Assert.AreEqual(0, collection.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //ClearTest
            collection.Add(kvp1);
            collection.Add(kvp2);
            Assert.AreEqual(2, collection.Count);
            MockEvent.AssertMockNotifiersDictionary(2);

            collection.Clear();
            Assert.AreEqual(0, collection.Count);
            MockEvent.AssertMockNotifiersDictionary(1);

            //Contains Test
            collection.Add(kvp1);
            Assert.IsTrue(collection.Contains(kvp1));
            Assert.IsFalse(collection.Contains(kvp2));

            //Syncroot Test
            Assert.IsFalse(collection.IsReadOnly);

            //IEnumerator/ EnumeratorGeneric test
            collection.Clear();
            collection.Add(kvp1);
            collection.Add(kvp2);
            collection.Add(kvp3);
            Assert.AreEqual(3, collection.Count);

            IEnumerator enumerator = collection.GetEnumerator();
            Assert.IsNotNull(enumerator);
            enumerator.MoveNext();
            Assert.AreEqual(kvp1.Key, ((KeyValuePair<string, TestItem>) enumerator.Current).Key);
            Assert.AreEqual(kvp1.Value, ((KeyValuePair<string, TestItem>) enumerator.Current).Value);

            IEnumerator<KeyValuePair<string,TestItem>> enumeratorGeneric = collection.GetEnumerator();
            Assert.IsNotNull(enumeratorGeneric);
            enumeratorGeneric.MoveNext();
            Assert.AreEqual(kvp1.Key, enumeratorGeneric.Current.Key);
            Assert.AreEqual(kvp1.Value, enumeratorGeneric.Current.Value);

            //CopyTo test
            var array = new KeyValuePair<string, TestItem>[3];
            collection.CopyTo(array, 0);
            Assert.AreEqual(array[0], kvp1);
            Assert.AreEqual(array[1], kvp2);
            Assert.AreEqual(array[2], kvp3);
        }
    }

    /// <summary>
    /// A Test item used in these tests
    /// </summary>
    public class TestItem {
        private static Fixture Fixture { get; } = new Fixture();

        public string Id { get; set; }

        public TestItem() {
            Id = Fixture.Create<string>();
        }
    }
}
