using AutoFixture;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test.Standard {

    [TestFixture]
    public class ObservableSortedListTestInterface {

        public static Fixture Fixture = new Fixture();

        public ObservableSortedList<string, TestItem> Observable;

        private readonly InterfaceTestCases _testCases = new InterfaceTestCases();

        [SetUp]
        public void TestInit() {
            _testCases.TestInit();
            Observable = new ObservableSortedList<string, TestItem>();
        }

        [Test]
        public void Collection() {
            Observable.Add(Fixture.Create<string>(), Fixture.Create<TestItem>());
            Observable.Add(Fixture.Create<string>(), Fixture.Create<TestItem>());
            Observable.Add(Fixture.Create<string>(), Fixture.Create<TestItem>());
            _testCases.CollectionTest(Observable);
        }

        //Does not implement
        //[Test] 
        //public void CollectionGeneric() => _testCases.CollectionGenericTest(ObservableSortedList);

        [Test]
        public void CollectionKvp() => _testCases.CollectionKeyValuePairTest(Observable);

        [Test]
        public void Dictionary() => _testCases.DictionaryTest(Observable);

        [Test]
        public void DictionaryGeneric() => _testCases.DictionaryGenericTest(Observable);

        //[Test]
        //public void ListInterface() => _testCases.ListTest(Observable);

    }
}
