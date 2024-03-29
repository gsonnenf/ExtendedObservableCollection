﻿using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gstc.Collections.Observable.Test.Standard {
    [TestFixture]
    public class ObservableListTest : CollectionTestBase<object, object> {

        private ObservableList<object> ObvList { get; set; }

        [SetUp]
        public new void TestInit() {
            base.TestInit();
            ObvList = new ObservableList<object>();
        }

        [Test, Description("")]
        public void TestMethod_InitList() {
            List<object> list = new List<object> { Item1, Item2, Item3 };

            ObvList = new ObservableList<object>(list);

            Assert.That(ObvList.Count == 3);
            Assert.That(ObvList[0] == Item1);
            Assert.That(ObvList[1] == Item2);
            Assert.That(ObvList[2] == Item3);
        }

        [Test, Description("")]
        public void TestMethod_ListAdd() {

            List<object> list = new List<object> { Item1, Item2, Item3 };

            ObvList.CollectionChanged += AssertCollectionEventReset;
            AddMockNotifiers();

            ObvList.List = list;

            AssertMockNotifiers(2, 1);
            Assert.That(ObvList.Count == 3);
            Assert.That(ObvList[0] == Item1);
            Assert.That(ObvList[1] == Item2);
            Assert.That(ObvList[2] == Item3);

        }

        [Test, Description("")]
        public void TestMethod_Add() {

            ObvList.CollectionChanged += GenerateAssertCollectionEventAddOne(0, DefaultTestItem);
            AddMockNotifiers();

            ObvList.Add(DefaultTestItem);

            AssertMockNotifiers(2, 1);
            Assert.That(ObvList.Count == 1);
            Assert.That(ObvList[0] == DefaultTestItem);

        }

        [Test, Description("")]
        public void TestMethod_AddRange() {

            ObvList.Add(DefaultTestItem);

            ObvList.CollectionChanged += GenerateAssertCollectionEventAddThree(1, Item1, Item2, Item3);
            AddMockNotifiers();

            ObvList.AddRange(new List<object>() { Item1, Item2, Item3 });

            AssertMockNotifiers(2, 1);
            Assert.That(ObvList.Count, Is.EqualTo(4));
            Assert.That(ObvList[0], Is.EqualTo(DefaultTestItem));
            Assert.That(ObvList[1], Is.EqualTo(Item1));
            Assert.That(ObvList[2], Is.EqualTo(Item2));
            Assert.That(ObvList[3], Is.EqualTo(Item3));
        }

        [Test, Description("")]
        public void TestMethod_ReplaceList() {

            ObvList.Add(DefaultTestItem);
            ObvList.Add(UpdateTestItem);

            List<object> list = new List<object> { Item1, Item2, Item3 };

            ObvList.CollectionChanged += AssertCollectionEventReset;
            AddMockNotifiers();

            ObvList.List = list;

            AssertMockNotifiers(2, 1);
            Assert.That(ObvList.Count == 3);
            Assert.That(ObvList[0] == Item1);
            Assert.That(ObvList[1] == Item2);
            Assert.That(ObvList[2] == Item3);
        }

        [Test, Description("")]
        public void TestMethod_Remove() {

            ObvList.List = new List<object> { Item1, Item2, Item3 };

            ObvList.CollectionChanged += GenerateAssertCollectionEventRemoveOne(1, Item2);
            AddMockNotifiers();

            ObvList.Remove(Item2);

            AssertMockNotifiers(2, 1);
            Assert.That(ObvList.Count == 2);
            Assert.That(ObvList[0] == Item1);
            Assert.That(ObvList[1] == Item3);
        }

        [Test, Description("")]
        public void TestMethod_RemoveAt() {

            ObvList.List = new List<object> { Item1, Item2, Item3 };

            ObvList.CollectionChanged += GenerateAssertCollectionEventRemoveOne(1, Item2);
            AddMockNotifiers();

            ObvList.RemoveAt(1);

            AssertMockNotifiers(2, 1);
            Assert.That(ObvList.Count == 2);
            Assert.That(ObvList[0] == Item1);
            Assert.That(ObvList[1] == Item3);
        }

        /// <summary>
        /// Utility stuff
        /// </summary>
        private void AddMockNotifiers() {
            //Sets up event testers
            ObvList.PropertyChanged += (sender, args) => AssertEvent.Call("PropertyChanged");
            ObvList.CollectionChanged += (sender, args) => AssertEvent.Call("CollectionChanged");
        }

        private void AssertMockNotifiers(int timesPropertyCalled, int timesCollectionCalled) {
            MockEvent.Verify(m => m.Call("PropertyChanged"), Times.Exactly(timesPropertyCalled));
            MockEvent.Verify(m => m.Call("CollectionChanged"), Times.Exactly(timesCollectionCalled));
        }
    }
}
