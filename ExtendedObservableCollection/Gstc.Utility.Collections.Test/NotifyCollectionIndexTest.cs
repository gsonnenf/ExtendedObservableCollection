
using System;
using System.Collections.Generic;
using AutoFixture;
using Moq;
using NUnit.Framework;
using Gstc.Collections.Observable;

namespace Gstc.Collections.Observable.Test {
    [TestFixture]
    public class NotifyCollectionIndexTest : TestUtils<object,object> {

        private ObservableSortedList<object, object> ObvDictionary { get; set; }

        [SetUp]
        public new void TestInit() {
            base.TestInit();
            ObvDictionary = new ObservableSortedList<object, object>();
        }

        [Test, Description("Remove Key/item using key, returns correct index.")]
        public void TestMethod_RemoveWithIndex() {

            var key1 = "a"; //0
            var key2 = "c"; //2 
            var key3 = "b"; //1

            var value1 = Fixture.Create<string>();
            var value2 = Fixture.Create<string>();
            var value3 = Fixture.Create<string>();

            ObvDictionary.Add(key1, value1);
            ObvDictionary.Add(key2, value2);
            ObvDictionary.Add(key3, value3);
                   
            Log(ObvDictionary.SortedList.IndexOfKey(key1));
            Log(ObvDictionary.SortedList.IndexOfKey(key2));
            Log(ObvDictionary.SortedList.IndexOfKey(key3));
            

            ObvDictionary.CollectionChanged += (sender, args) => {                         
                Assert.That(args.OldItems[0], Is.EqualTo(value2));
                Assert.That(args.OldStartingIndex, Is.EqualTo(2));
                Assert.That(args.NewItems, Is.Null);
                Assert.That(args.NewStartingIndex, Is.EqualTo(-1));
                AssertEvent.Call("collection");
            };
            
            ObvDictionary.Remove(key2);

            MockEvent.Verify(m => m.Call("collection"), Times.Exactly(1));

        }
    }
}

