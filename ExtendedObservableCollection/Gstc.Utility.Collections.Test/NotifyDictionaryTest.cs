using System;
using System.Collections.Generic;
using AutoFixture;
using Moq;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {
    [TestFixture]
    public class NotifyDictionaryTest : TestUtils<object,object> {
      
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

        private ObservableDictionary<object, object> ObvDictionary { get; set; }

        [SetUp]
        public new void TestInit() {
            base.TestInit();
            ObvDictionary = new ObservableDictionary<object, object>();           
        }

        [Test, TestCaseSource(nameof(DefaultStaticValue)), Description("Add key/item using Add")]
        public void TestMethod_Add(object item) {

            ObvDictionary.DictionaryChanged += GenerateAssertDictionaryEventAddOne(DefaultKey, item);
            AddMockEventNotifiers();

            ObvDictionary.Add(DefaultKey, item);
            
            AssertMockEventNotifiers(2,1);
        }

        [Test, TestCaseSource(nameof(DefaultStaticValue)), Description("Replace value using index")]
        public void TestMethod_AddIndexer(object item) {

            ObvDictionary.DictionaryChanged += GenerateAssertDictionaryEventAddOne(DefaultKey, item);
            AddMockEventNotifiers();

            ObvDictionary[DefaultKey] = item;

            AssertMockEventNotifiers(2, 1);          
        }

        [Test, Description("")]
        public void TestMethod_Clear() {

            ObvDictionary.Add(DefaultKey, DefaultValue);

            ObvDictionary.DictionaryChanged += AssertDictionaryEventReset;
            AddMockEventNotifiers();

            ObvDictionary.Clear();

            AssertMockEventNotifiers(2, 1);
            Assert.That(ObvDictionary.Count, Is.EqualTo(0));          
        }


        [Test, Description("")]
        public void TestMethod_ReplaceDictionary() {

            ObvDictionary.Add(DefaultKey, DefaultValue);

            ObvDictionary.DictionaryChanged += AssertDictionaryEventReset;
            AddMockEventNotifiers();

            ObvDictionary.Dictionary = new Dictionary<object, object>();

            AssertMockEventNotifiers(2, 1);
            Assert.That( ObvDictionary.Count, Is.EqualTo(0));
        }

        [Test, Description("Remove Key/item using key")]
        public void TestMethod_Remove() {

            ObvDictionary.Add(DefaultKey, DefaultValue);

            ObvDictionary.DictionaryChanged += GenerateAssertDictionaryEventRemoveOne(DefaultKey, DefaultValue);
            AddMockEventNotifiers();

            ObvDictionary.Remove(DefaultKey);

            AssertMockEventNotifiers(2, 1);           
        }

        [Test, Description("Replace value using index")]
        public void TestMethod_ReplaceValue() {
           
            ObvDictionary.Add(DefaultKey, DefaultValue);

            ObvDictionary.DictionaryChanged += (sender, args) => {
                Assert.That(args.Action == NotifyDictionaryChangedAction.Replace);
                Assert.That(args.OldKeys[0], Is.EqualTo(DefaultKey));
                Assert.That(args.OldItems[0], Is.EqualTo(DefaultValue));
                Assert.That(args.NewKeys[0], Is.EqualTo(DefaultKey));
                Assert.That(args.NewItems[0], Is.EqualTo(UpdateValue));
                AssertEvent.Call("DictionaryChanged");
            };

            ObvDictionary[DefaultKey] = UpdateValue;

            MockEvent.Verify(m => m.Call("DictionaryChanged"), Times.Exactly(1));           
        }

        [Test, Description("")]
        public void TestMethod_NullKey() {

            //Fails if null keys trigger an event.
            ObvDictionary.DictionaryChanged += (sender, args) => Assert.Fail();         
            Assert.Throws<ArgumentNullException>(() => ObvDictionary.Add(null, DefaultValue));
            Assert.Throws<ArgumentNullException>(() => ObvDictionary[null] = DefaultValue);
        }

        [Test, Description("")]
        public void TestMethod_DuplicateKey() {

            ObvDictionary.Add(DefaultKey, DefaultValue);

            //Fails if adding a duplicate triggers triggers an event.
            ObvDictionary.DictionaryChanged += (sender, args) => Assert.Fail();          
            Assert.Throws<ArgumentException>(() => ObvDictionary.Add(DefaultKey, DefaultValue));
        }

        #region Test Specific Utility
        private void AddMockEventNotifiers() {
            //Sets up event testers
            ObvDictionary.PropertyChanged += (sender, args) => AssertEvent.Call("PropertyChanged");
            ObvDictionary.DictionaryChanged += (sender, args) => AssertEvent.Call("DictionaryChanged");
        }

        private void AssertMockEventNotifiers(int timesPropertyCalled, int timesDictionaryCalled) {
            //Sets up event testers
            MockEvent.Verify(m => m.Call("PropertyChanged"), Times.Exactly(timesPropertyCalled));
            MockEvent.Verify(m => m.Call("DictionaryChanged"), Times.Exactly(timesDictionaryCalled));
        }

        #endregion

    }
}
