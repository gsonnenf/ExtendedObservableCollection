using System.Linq;
using AutoFixture;
using Gstc.Collections.Observable.Base;
using Gstc.Collections.Observable.Interface;
using Gstc.Collections.Observable.Nonstandard;
using Gstc.Collections.Observable.Standard;
using Moq;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {

    [TestFixture]
    public class ObservableCollectionAdapterTest : CollectionTestBase<object, string> {
        private BaseObservableList<string> TestBaseList { get; set; }
        private TestAdapterClass TestAdapter { get; set; }
      
        [SetUp]
        public new void TestInit() {
            base.TestInit();
            TestBaseList = new ObservableList<string>();
        }

        [Test, Description("")]
        public void TestMethod_Set2() {

            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            TestAdapter.SourceCollection = TestBaseList;

            Assert.That(TestAdapter.Count, Is.EqualTo(3));
            Assert.That(Item1, Is.EqualTo(TestAdapter.First().BaseString));
            Assert.That(Item2, Is.EqualTo(TestAdapter[1].BaseString));
            Assert.That(Item3, Is.EqualTo(TestAdapter[2].BaseString));
            Assert.That(Item1 + TestAdapter.First().AddedString, Is.EqualTo(TestAdapter.First().StringView));
        }

        [Test, Description("")]
        public void TestMethod_SetConstructorAndAdd() {
            TestAdapter = new TestAdapterClass(TestBaseList);

            TestBaseList.PropertyChanged += (sender, args) => AssertEvent.Call("PropertyChanged");
            TestBaseList.CollectionChanged += (sender, args) => AssertEvent.Call("CollectionChanged");

            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            Assert.That(TestAdapter.Count, Is.EqualTo(3));
            Assert.That(Item1, Is.EqualTo(TestAdapter.First().BaseString));
            Assert.That(Item2, Is.EqualTo(TestAdapter[1].BaseString));
            Assert.That(Item3, Is.EqualTo(TestAdapter[2].BaseString));
            Assert.That(Item1 + TestAdapter.First().AddedString, Is.EqualTo(TestAdapter.First().StringView));

            MockEvent.Verify(m => m.Call("PropertyChanged"), Times.Exactly(6));
            MockEvent.Verify(m => m.Call("CollectionChanged"), Times.Exactly(3));
        }

        [Test, Description("")]
        public void TestMethod_Set4() {

            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            TestAdapter = new TestAdapterClass(TestBaseList);

            Assert.That(TestAdapter.Count, Is.EqualTo(3));
            Assert.That(Item1, Is.EqualTo(TestAdapter.First().BaseString));
            Assert.That(Item2, Is.EqualTo(TestAdapter[1].BaseString));
            Assert.That(Item3, Is.EqualTo(TestAdapter[2].BaseString));
            Assert.That(Item1 + TestAdapter.First().AddedString, Is.EqualTo(TestAdapter.First().StringView));
        }

        [Test, Description("")]
        public void TestMethod_SetAndAdd() {
            TestAdapter.SourceCollection = TestBaseList;

            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            Assert.That(TestAdapter.Count, Is.EqualTo(3));
            Assert.That(Item1, Is.EqualTo(TestAdapter.First().BaseString));
            Assert.That(Item2, Is.EqualTo(TestAdapter[1].BaseString));
            Assert.That(Item3, Is.EqualTo(TestAdapter[2].BaseString));
            Assert.That(Item1 + TestAdapter.First().AddedString, Is.EqualTo(TestAdapter.First().StringView));
        }

        [Test, Description("")]
        public void TestMethod_ReplaceList() {
            var testListA = new ObservableList<string>();

            testListA.Add(Item1);
            testListA.Add(Item2);
            testListA.Add(Item3);

            var testListB = new ObservableList<string>();
            var item1B = Fixture.Create<string>();
            var item2B = Fixture.Create<string>();
            testListB.Add(item1B);
            testListB.Add(item2B);

            TestAdapter.SourceCollection = testListA;

            Assert.That(TestAdapter.Count, Is.EqualTo(3));
            Assert.That(Item1 == TestAdapter[0].BaseString);
            Assert.That(Item2 == TestAdapter[1].BaseString);
            Assert.That(Item3 == TestAdapter[2].BaseString);
            

            TestAdapter.SourceCollection = testListB;

            Assert.That(item1B == TestAdapter[0].BaseString);
            Assert.That(item2B == TestAdapter[1].BaseString);

            Assert.That(TestAdapter.Count, Is.EqualTo(2));
        }


        [Test, Description("")]
        public void TestMethod_Remove() {

            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            TestAdapter = new TestAdapterClass(TestBaseList);

            Assert.That(TestAdapter.Count == 3);

            TestBaseList.RemoveAt(1);

            Assert.That(TestAdapter.Count == 2);
            Assert.That(TestAdapter[0].BaseString == Item1);
            Assert.That(TestAdapter[1].BaseString == Item3);
        }

        [Test, Description("")]
        public void TestMethod_Move() {

            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            TestAdapter = new TestAdapterClass(TestBaseList);

            Assert.That(TestAdapter.Count == 3);

            TestBaseList.Move(1,0);

            Assert.That(TestAdapter.Count == 3);
            Assert.That(TestAdapter[0].BaseString == Item2);
            Assert.That(TestAdapter[1].BaseString == Item1);
            Assert.That(TestAdapter[2].BaseString == Item3);
        }


        [Test, Description("")]
        public void TestMethod_Replace() {

            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            TestAdapter = new TestAdapterClass(TestBaseList);

            Assert.That(TestAdapter.Count == 3);

            var item4 = Fixture.Create<string>();
            TestBaseList[1] = item4;

            Assert.That(TestAdapter.Count == 3);
            Assert.That(TestAdapter[0].BaseString == Item1);
            Assert.That(TestAdapter[1].BaseString == item4);
            Assert.That(TestAdapter[2].BaseString == Item3);
        }

        [Test, Description("")]
        public void TestMethod_Clear() {
  
            TestBaseList.Add(Item1);
            TestBaseList.Add(Item2);
            TestBaseList.Add(Item3);

            TestAdapter = new TestAdapterClass(TestBaseList);

            Assert.That( TestAdapter.Count == 3 );

            TestBaseList.Clear();

            Assert.That( TestAdapter.Count == 0 );
        }


        #region Test Class Definitions
        /// <summary>
        /// Items used in test.
        /// </summary>
        public class TestItemClass {

            //public static string AddedString = ":ViewModel";
            public static Fixture Fixture = new Fixture();

            public TestItemClass(string myString) {
                BaseString = myString;
                AddedString = Fixture.Create<string>();
                StringView = myString + AddedString;
            }
            public string StringView { get; set; }
            public string BaseString { get; set; }
            public string AddedString { get; set; }

        }

        public class TestAdapterClass : ObservableListAdapter<string, TestItemClass> {

            public TestAdapterClass() : base() { }

            public TestAdapterClass(IObservableCollection<string> sourceCollection) : base(sourceCollection) {}

            public override string Convert(TestItemClass itemClass) => itemClass.StringView;

            public override TestItemClass Convert(string item) => new TestItemClass( item );
        }
        #endregion
    }
}
