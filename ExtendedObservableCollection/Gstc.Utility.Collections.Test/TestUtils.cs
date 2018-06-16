using System;
using System.Collections.Specialized;
using AutoFixture;
using Gstc.Collections.Observable.Base;
using Moq;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {
    public class TestUtils<TKey, TItem> {

        #region Default Test Tools
        protected static Fixture Fixture { get; } = new Fixture();
        protected MockEventClass MockEvent { get; set; }
        protected AssertEventClass AssertEvent { get; set; }
        #endregion

        #region Default test items

        protected TItem DefaultTestItem { get; set; }
        protected TItem UpdateTestItem { get; set; }

        protected TItem Item1 { get; set; }
        protected TItem Item2 { get; set; }
        protected TItem Item3 { get; set; }

        protected TKey DefaultKey { get; set; }
        protected TItem DefaultValue { get; set; }
        protected TKey UpdateKey { get; set; }
        protected TItem UpdateValue { get; set; }

        #endregion

        public void TestInit() {
            MockEvent = new MockEventClass();
            AssertEvent = MockEvent.Object;

            //Generalize mock data
            DefaultTestItem = Fixture.Create<TItem>();
            UpdateTestItem = Fixture.Create<TItem>();
            Item1 = Fixture.Create<TItem>();
            Item2 = Fixture.Create<TItem>();
            Item3 = Fixture.Create<TItem>();

            DefaultKey = Fixture.Create<TKey>();
            DefaultValue = Fixture.Create<TItem>();
            UpdateKey = Fixture.Create<TKey>();
            UpdateValue = Fixture.Create<TItem>();
        }

        #region Collection Event Args Tests
        protected void AssertCollectionEventReset(object sender, NotifyCollectionChangedEventArgs args) {
            Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Reset));
            Assert.That(args.OldItems, Is.Null);
            Assert.That(args.NewItems, Is.Null);
            Assert.That(args.OldStartingIndex == -1);
            Assert.That(args.NewStartingIndex == -1);
        }

        protected NotifyCollectionChangedEventHandler GenerateAssertCollectionEventAddOne(int index,TItem item) {
            return (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
                Assert.That(args.OldStartingIndex == -1);
                Assert.That(args.NewStartingIndex == index);               
                Assert.That(args.OldItems, Is.Null);
                Assert.That(args.NewItems[0], Is.EqualTo(item));              
            };
        }

        protected NotifyCollectionChangedEventHandler GenerateAssertCollectionEventAddThree(int index, TItem item1, TItem item2, TItem item3) {
            return (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Add));
                Assert.That(args.OldStartingIndex, Is.EqualTo(-1));
                Assert.That(args.NewStartingIndex, Is.EqualTo(index));
                Assert.That(args.OldItems, Is.Null);
                Assert.That(args.NewItems[0], Is.EqualTo(item1));
                Assert.That(args.NewItems[1], Is.EqualTo(item2));
                Assert.That(args.NewItems[2], Is.EqualTo(item3));
            };
        }

        

        protected NotifyCollectionChangedEventHandler GenerateAssertCollectionEventRemoveOne(int index, TItem item) {
            return (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyCollectionChangedAction.Remove));
                Assert.That(args.OldStartingIndex == index);
                Assert.That(args.NewStartingIndex == -1);
                Assert.That(args.OldItems[0], Is.EqualTo(item));              
                Assert.That(args.NewItems, Is.Null);
            };
        }
#endregion

        #region Dictionary Event Arg Tests
        protected void AssertDictionaryEventReset(object sender, NotifyDictionaryChangedEventArgs<TKey, TItem> args) {
            Assert.That(args.Action, Is.EqualTo(NotifyDictionaryChangedAction.Reset));
            Assert.That(args.OldItems, Is.Null);
            Assert.That(args.NewItems, Is.Null);
            Assert.That(args.OldKeys, Is.Null);
            Assert.That(args.NewKeys, Is.Null);
        }

        protected NotifyDictionaryChangedEventHandler<TKey, TItem> GenerateAssertDictionaryEventAddOne(TKey key, TItem item) {
            return (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyDictionaryChangedAction.Add));
                Assert.That(args.OldItems, Is.Null);
                Assert.That(args.OldKeys, Is.Null);
                Assert.That(args.NewKeys[0], Is.EqualTo(key));
                Assert.That(args.NewItems[0], Is.EqualTo(item));
            };
        }

        protected NotifyDictionaryChangedEventHandler<TKey, TItem> GenerateAssertDictionaryEventRemoveOne(TKey key, TItem item) {
            return (sender, args) => {
                Assert.That(args.Action, Is.EqualTo(NotifyDictionaryChangedAction.Remove));
                Assert.That(args.NewKeys, Is.Null);
                Assert.That(args.NewItems, Is.Null);
                Assert.That(args.OldKeys[0], Is.EqualTo(key));
                Assert.That(args.OldItems[0], Is.EqualTo(item));
            };
        }
        #endregion

        protected void Log(object message) => Console.WriteLine(message);

        public class MockEventClass : Mock<AssertEventClass> { }

        public class AssertEventClass { public virtual void Call(string obj) { } }
    }
}
