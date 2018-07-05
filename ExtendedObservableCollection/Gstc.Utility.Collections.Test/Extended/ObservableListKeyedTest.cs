using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using AutoFixture;
using Gstc.Collections.Observable.Extended;
using Gstc.Collections.Observable.Notify;
using Moq;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test.Extended {
    [TestFixture]
    public class ObservableListKeyedTest : CollectionTestBase<string, ObservableListKeyedTest.TestItemClass> {

        private ListKeyedImpl ObvKeyedList { get; set; }

        [SetUp]
        public new void TestInit() {
            base.TestInit();
            ObvKeyedList = new ListKeyedImpl();
        }

        [Test, Description("")]
        public void TestMethod_Add() {

            AddMockEventNotifiers();

            ObvKeyedList.CollectionChanged += GenerateAssertCollectionEventAddOne(0, DefaultTestItem);
            ObvKeyedList.DictionaryChanged += GenerateAssertDictionaryEventAddOne(DefaultTestItem.Key, DefaultTestItem);

            ObvKeyedList.Add(DefaultTestItem);

            Assert.That(ObvKeyedList.Count, Is.EqualTo(1));
            Assert.That(ObvKeyedList[DefaultTestItem.Key], Is.EqualTo(DefaultTestItem));
            Assert.That(ObvKeyedList.IndexOf(DefaultTestItem), Is.EqualTo(0));
            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, Description("")]
        public void TestMethod_AddMultiple() {

            AddMockEventNotifiers();

            ObvKeyedList.Add(Item1);
            ObvKeyedList.Add(Item2);
            ObvKeyedList.Add(Item3);

            Assert.That(ObvKeyedList.Count, Is.EqualTo(3));
            Assert.That(ObvKeyedList[Item1.Key] == Item1);
            Assert.That(ObvKeyedList[Item2.Key] == Item2);
            Assert.That(ObvKeyedList[Item3.Key] == Item3);

            Assert.That(ObvKeyedList.IndexOf(Item1) == 0);
            Assert.That(ObvKeyedList.IndexOf(Item2) == 1);
            Assert.That(ObvKeyedList.IndexOf(Item3) == 2);

            AssertMockEventNotifiers(6, 3, 3);
        }

        [Test, Description("")]
        public void TestMethod_ListAdd() {

            List<TestItemClass> list = new List<TestItemClass> { DefaultTestItem };

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += AssertCollectionEventReset;
            ObvKeyedList.DictionaryChanged += AssertDictionaryEventReset;

            ObvKeyedList.List = list;

            Assert.That(ObvKeyedList.Count, Is.EqualTo(1));
            Assert.That(ObvKeyedList[DefaultTestItem.Key] == DefaultTestItem);
            AssertMockEventNotifiers(2, 1, 1);
        }



        [Test, Description("")]
        public void TestMethod_ListAddMultiple() {

            List<TestItemClass> list = new List<TestItemClass> { Item1, Item2, Item3 };

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += AssertCollectionEventReset;
            ObvKeyedList.DictionaryChanged += AssertDictionaryEventReset;

            ObvKeyedList.List = list;

            Assert.That(ObvKeyedList.Count, Is.EqualTo(3));
            Assert.That(ObvKeyedList[Item1.Key], Is.EqualTo(Item1));
            Assert.That(ObvKeyedList[Item2.Key], Is.EqualTo(Item2));
            Assert.That(ObvKeyedList[Item3.Key], Is.EqualTo(Item3));

            Assert.That(ObvKeyedList.IndexOf(Item1) == 0);
            Assert.That(ObvKeyedList.IndexOf(Item2) == 1);
            Assert.That(ObvKeyedList.IndexOf(Item3) == 2);

            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, Description("")]
        public void TestMethod_AddWithKey() {

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += GenerateAssertCollectionEventAddOne(0, DefaultTestItem);
            ObvKeyedList.DictionaryChanged += GenerateAssertDictionaryEventAddOne(DefaultTestItem.Key, DefaultTestItem);

            Assert.Throws<ArgumentException>(() => ObvKeyedList.Add("invalidKey", DefaultTestItem));
            ObvKeyedList.Add(DefaultTestItem.Key, DefaultTestItem);

            AssertMockEventNotifiers(2, 1, 1);
        }


        [Test, Description("")]
        public void TestMethod_Clear() {

            ObvKeyedList.List = new List<TestItemClass>() { Item1, Item2, Item3 };

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += AssertCollectionEventReset;
            ObvKeyedList.DictionaryChanged += AssertDictionaryEventReset;

            ObvKeyedList.Clear();

            Assert.That(ObvKeyedList.Count, Is.EqualTo(0));
            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, Description("")]
        public void TestMethod_RemoveItem() {
            ObvKeyedList.List = new List<TestItemClass>() { Item1, Item2, Item3 };

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += GenerateAssertCollectionEventRemoveOne(1, Item2);
            ObvKeyedList.DictionaryChanged += GenerateAssertDictionaryEventRemoveOne(Item2.Key, Item2);

            ObvKeyedList.Remove(Item2);

            Assert.That(ObvKeyedList.Count, Is.EqualTo(2));
            Assert.That(ObvKeyedList[Item1.Key] == Item1);
            Assert.That(ObvKeyedList[Item3.Key] == Item3);
            Assert.That(ObvKeyedList.IndexOf(Item1) == 0);
            Assert.That(ObvKeyedList.IndexOf(Item3) == 1);
            Assert.Throws<KeyNotFoundException>(delegate { if (ObvKeyedList[Item2.Key] != null) Assert.Fail(); });
            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, Description("")]
        public void TestMethod_RemoveKey() {
            ObvKeyedList.List = new List<TestItemClass>() { Item1, Item2, Item3 };

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += GenerateAssertCollectionEventRemoveOne(1, Item2);
            ObvKeyedList.DictionaryChanged += GenerateAssertDictionaryEventRemoveOne(Item2.Key, Item2);

            ObvKeyedList.Remove(Item2.Key);

            Assert.That(ObvKeyedList.Count, Is.EqualTo(2));
            Assert.That(ObvKeyedList[Item1.Key] == Item1);
            Assert.That(ObvKeyedList[Item3.Key] == Item3);
            Assert.That(ObvKeyedList.IndexOf(Item1) == 0);
            Assert.That(ObvKeyedList.IndexOf(Item3) == 1);
            Assert.Throws<KeyNotFoundException>(delegate { if (ObvKeyedList[Item2.Key] != null) Assert.Fail(); });
            AssertMockEventNotifiers(2, 1, 1);

        }

        [Test, Description("")]
        public void TestMethod_RemoveAt() {
            ObvKeyedList.List = new List<TestItemClass>() { Item1, Item2, Item3 };

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += GenerateAssertCollectionEventRemoveOne(1, Item2);
            ObvKeyedList.DictionaryChanged += GenerateAssertDictionaryEventRemoveOne(Item2.Key, Item2);

            ObvKeyedList.RemoveAt(1);

            Assert.That(ObvKeyedList.Count, Is.EqualTo(2));
            Assert.That(ObvKeyedList[Item1.Key] == Item1);
            Assert.That(ObvKeyedList[Item3.Key] == Item3);
            Assert.That(ObvKeyedList.IndexOf(Item1) == 0);
            Assert.That(ObvKeyedList.IndexOf(Item3) == 1);
            Assert.Throws<KeyNotFoundException>(delegate { if (ObvKeyedList[Item2.Key] != null) Assert.Fail(); });
            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, Description("")]
        public void TestMethod_Replace() {

            ObvKeyedList.List = new List<TestItemClass>() { Item1, Item2, Item3 };

            ObvKeyedList.CollectionChanged += (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Replace));
                Assert.That(args.OldItems[0] == Item2);
                Assert.That(args.NewItems[0] == UpdateTestItem);
                Assert.That(args.OldStartingIndex == 1);
                Assert.That(args.NewStartingIndex == 1);
            };

            ObvKeyedList.DictionaryChanged += (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyDictionaryChangedAction.Replace));
                Assert.That(args.OldItems[0] == Item2);
                Assert.That(args.OldKeys[0] == Item2.Key);
                Assert.That(args.NewItems[0] == UpdateTestItem);
                Assert.That(args.NewKeys[0] == UpdateTestItem.Key);
            };

            AddMockEventNotifiers();

            var key = Item2.Key;

            Assert.Throws<ArgumentException>(() => ObvKeyedList[key] = UpdateTestItem);

            UpdateTestItem.Key = key;
            ObvKeyedList[key] = UpdateTestItem;

            Assert.That(ObvKeyedList[key].Item == UpdateTestItem.Item);
            Assert.That(ObvKeyedList[key].Item != Item2.Item);

            AssertMockEventNotifiers(1, 1, 1);
        }


        [Test, Description("")]
        public void TestMethod_Insert() {

            ObvKeyedList.List = new List<TestItemClass>() { Item1, Item2, Item3 };

            AddMockEventNotifiers();
            ObvKeyedList.CollectionChanged += GenerateAssertCollectionEventAddOne(1, UpdateTestItem);
            ObvKeyedList.DictionaryChanged += GenerateAssertDictionaryEventAddOne(UpdateTestItem.Key, UpdateTestItem);

            ObvKeyedList.Insert(1, UpdateTestItem);

            Assert.That(ObvKeyedList.IndexOf(Item1) == 0);
            Assert.That(ObvKeyedList.IndexOf(UpdateTestItem) == 1);
            Assert.That(ObvKeyedList.IndexOf(Item2) == 2);
            Assert.That(ObvKeyedList.IndexOf(Item3) == 3);

            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, Description("")]
        public void TestMethod_Move() {

            ObvKeyedList.List = new List<TestItemClass>() { Item1, Item2, Item3 };

            AddMockEventNotifiers();

            ObvKeyedList.CollectionChanged += (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Move));
                Assert.That(args.OldItems[0] == Item1);
                Assert.That(args.NewItems[0] == Item1);
                Assert.That(args.OldStartingIndex == 0);
                Assert.That(args.NewStartingIndex == 1);
            };

            ObvKeyedList.Move(0, 1);

            Assert.That(ObvKeyedList.IndexOf(Item1) == 1);
            Assert.That(ObvKeyedList.IndexOf(Item2) == 0);
            Assert.That(ObvKeyedList.IndexOf(Item3) == 2);


            AssertMockEventNotifiers(1, 1, 0);
        }

        #region Test Specific Utility
        private void AddMockEventNotifiers() {
            //Sets up event testers
            ObvKeyedList.PropertyChanged += (sender, args) => AssertEvent.Call("PropertyChanged");
            ObvKeyedList.CollectionChanged += (sender, args) => AssertEvent.Call("CollectionChanged");
            ObvKeyedList.DictionaryChanged += (sender, args) => AssertEvent.Call("DictionaryChanged");
        }

        private void AssertMockEventNotifiers(int timesPropertyCalled, int timesCollectionCalled, int timesDictionaryCalled) {
            //Sets up event testers
            MockEvent.Verify(m => m.Call("PropertyChanged"), Times.Exactly(timesPropertyCalled));
            MockEvent.Verify(m => m.Call("CollectionChanged"), Times.Exactly(timesCollectionCalled));
            MockEvent.Verify(m => m.Call("DictionaryChanged"), Times.Exactly(timesDictionaryCalled));
        }



        /// <summary>
        /// Test implementations of KeyedList
        /// </summary>
        public class ListKeyedImpl : ObservableListKeyed<string, TestItemClass> {
            public override string GetKey(TestItemClass item) => item.Key;
        }

        public class TestItemClass {
            public TestItemClass() {
                Key = Fixture.Create<string>();
                Item = Fixture.Create<string>();
            }
            public string Key { get; set; }
            public string Item { get; set; }
        }
        #endregion
    }
}
