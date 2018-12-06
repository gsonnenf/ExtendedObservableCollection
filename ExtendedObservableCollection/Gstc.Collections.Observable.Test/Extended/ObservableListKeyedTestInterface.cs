using Gstc.Collections.Observable.Extended;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test.Extended {
    [TestFixture]
    public class ObservableListKeyedTestInterface {

        public ObservableListKeyed<string,TestItem> Observable;

        private readonly InterfaceTestCases _testCases = new InterfaceTestCases();

        [SetUp]
        public void TestInit() {
            _testCases.TestInit();
            Observable = new ObservableListKeyedFunc<string,TestItem>( item => item.Id );
        }

        [Test]
        public void Collection() {
            //Test needs 3 test items.
            Observable.Add(new TestItem());
            Observable.Add(new TestItem());
            Observable.Add(new TestItem());
            _testCases.CollectionTest(Observable);
        }

        [Test]
        public void CollectionGeneric() => _testCases.CollectionGenericTest(Observable);

        [Test]
        public void CollectionKvp() => _testCases.CollectionKeyValuePairTest(Observable);

        [Test]
        public void Dictionary() => _testCases.DictionaryTest(Observable);

        [Test]
        public void DictionaryGeneric() => _testCases.DictionaryGenericTest(Observable);

        [Test]
        public void ListInterface() => _testCases.ListTest(Observable);

    }
}
