
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using AutoFixture;
using Gstc.Collections.Observable.Standard;
using Moq;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {
    [TestFixture]
    public class NotifySortedListTest : TestUtils<object,object> {

        private static object[] DefaultStaticKey { get; } = {
            Fixture.Create<object>(),
            Fixture.Create<string>()
        };
        private static object[] DefaultStaticValue { get; } = {
            null,
            Fixture.Create<object>(),          
            Fixture.Create<string>(),
            Fixture.Create<int>(),
            Fixture.Create<double>()
        };
              
        private ObservableSortedList<object, object> ObvSortedList { get; set; }


        [SetUp]
        public new void TestInit() {
            base.TestInit();          
            ObvSortedList = new ObservableSortedList<object, object>();
        }

        [Test, Description("")]
        public void TestMethod_Clear() {


            ObvSortedList.Add(DefaultKey, DefaultValue);

            ObvSortedList.CollectionChanged += AssertCollectionEventReset;
            ObvSortedList.DictionaryChanged += AssertDictionaryEventReset;
            AddMockEventNotifiers();

            ObvSortedList.Clear();

            AssertMockEventNotifiers(2,1,1);
            Assert.That(ObvSortedList.Count, Is.EqualTo(0));
            
        }

        
        [Test, Description("")]
        public void TestMethod_ReplaceDictionary() {

            ObvSortedList.Add(DefaultKey, DefaultValue);

            ObvSortedList.CollectionChanged += AssertCollectionEventReset;
            ObvSortedList.DictionaryChanged += AssertDictionaryEventReset;
            AddMockEventNotifiers();

            ObvSortedList.SortedList = new System.Collections.Generic.SortedList<object, object>();

            AssertMockEventNotifiers(2, 1, 1);
            Assert.That( ObvSortedList.Count, Is.EqualTo(0));
            
        }

        [Test, TestCaseSource(nameof(DefaultStaticValue)), Description("Add key/item using Add")]
        public void TestMethod_Add(object item) {

            ObvSortedList.CollectionChanged += GenerateAssertCollectionEventAddOne(0, item);
            ObvSortedList.DictionaryChanged += GenerateAssertDictionaryEventAddOne(DefaultKey, item);
            AddMockEventNotifiers();

            ObvSortedList.Add(DefaultKey, item);

            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, TestCaseSource(nameof(DefaultStaticValue)), Description("Add key/item using Add")]
        public void TestMethod_AddIndexer(object item) {


            ObvSortedList.CollectionChanged += GenerateAssertCollectionEventAddOne(0, item);
            ObvSortedList.DictionaryChanged += GenerateAssertDictionaryEventAddOne(DefaultKey, item);
            AddMockEventNotifiers();

            ObvSortedList[DefaultKey] = item;
            AssertMockEventNotifiers(2, 1, 1);
        }

        [Test, Description("Remove Key/item using key")]
        public void TestMethod_Remove() {

            ObvSortedList.Add(DefaultKey, DefaultValue);

            ObvSortedList.CollectionChanged += GenerateAssertCollectionEventRemoveOne(0, DefaultValue);
            ObvSortedList.DictionaryChanged += GenerateAssertDictionaryEventRemoveOne(DefaultKey, DefaultValue);
            AddMockEventNotifiers();

            ObvSortedList.Remove(DefaultKey);

            AssertMockEventNotifiers(2, 1, 1);
        }


        [Test, TestCaseSource(nameof(DefaultStaticValue)), Description("Replace value using index")]
        public void TestMethod_ReplaceValue(object value) {
          
            ObvSortedList.Add(DefaultKey, DefaultValue);
                      
            ObvSortedList.CollectionChanged += (sender, args) => {
                Assert.AreEqual(args.NewItems[0], value);
                Assert.AreEqual(args.OldItems[0], DefaultValue);
            };
            AddMockEventNotifiers();

            ObvSortedList[DefaultKey] = value;

            AssertMockEventNotifiers(1, 1, 1);
        }

        [Test, Description("")]
        public void TestMethod_NullKey() {

            //Fails if null keys trigger an event.
            ObvSortedList.PropertyChanged += (sender, args) => Assert.Fail();
            ObvSortedList.CollectionChanged += (sender, args) => Assert.Fail();

            Assert.Throws<ArgumentNullException>( ()=> ObvSortedList.Add(null, DefaultValue) );
            Assert.Throws<ArgumentNullException>( () => ObvSortedList[null] = DefaultValue );
        }

        [Test, Description("")]
        public void TestMethod_DuplicateKey() {

            ObvSortedList.Add(DefaultKey, DefaultValue);

            ObvSortedList.PropertyChanged += (sender, args) => Assert.Fail();
            ObvSortedList.CollectionChanged += (sender, args) => Assert.Fail();

            Assert.Throws<ArgumentException>(() => ObvSortedList.Add(DefaultKey, DefaultValue));           
        }

        #region Test Specific Utility
        private void AddMockEventNotifiers() {
            //Sets up event testers
            ObvSortedList.PropertyChanged += (sender, args) => AssertEvent.Call("PropertyChanged");
            ObvSortedList.CollectionChanged += (sender, args) => AssertEvent.Call("CollectionChanged");
            ObvSortedList.DictionaryChanged += (sender, args) => AssertEvent.Call("DictionaryChanged");
        }

        private void AssertMockEventNotifiers(int timesPropertyCalled, int timesCollectionCalled, int timesDictionaryCalled) {
            //Sets up event testers
            MockEvent.Verify(m => m.Call("PropertyChanged"), Times.Exactly(timesPropertyCalled));
            MockEvent.Verify(m => m.Call("CollectionChanged"), Times.Exactly(timesCollectionCalled));
            MockEvent.Verify(m => m.Call("DictionaryChanged"), Times.Exactly(timesDictionaryCalled));
        }

        #endregion


    }
  
}
