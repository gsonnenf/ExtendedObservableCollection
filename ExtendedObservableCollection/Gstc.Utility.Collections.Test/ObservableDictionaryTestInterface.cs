using AutoFixture;
using Gstc.Collections.Observable.Nonstandard;
using Gstc.Collections.Observable.Standard;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {

    [TestFixture]
    public class ObservableDictionaryTestInterface {

        public static Fixture Fixture = new Fixture();

        public ObservableDictionary<string, TestItem> ObservableDictionary;

        private readonly InterfaceTestCases _testCases = new InterfaceTestCases();

        [SetUp]
        public void TestInit() {
            _testCases.TestInit();
            ObservableDictionary = new ObservableDictionary<string, TestItem>();
        }

        [Test]
        public void Collection() {
            ObservableDictionary.Add(Fixture.Create<string>(),Fixture.Create<TestItem>());
            ObservableDictionary.Add(Fixture.Create<string>(), Fixture.Create<TestItem>());
            ObservableDictionary.Add(Fixture.Create<string>(), Fixture.Create<TestItem>());
            _testCases.CollectionTest(ObservableDictionary);
        }

        //[Test] Does not implement
        //public void CollectionGeneric() => _testCases.CollectionGenericTest(ObservableDictionary);

        [Test]
        public void CollectionKvp() => _testCases.CollectionKeyValuePairTest(ObservableDictionary);

        //[Test] Does not Implemenet
        //public void Dictionary() => _testCases.DictionaryTest(ObservableDictionary);

        [Test]
        public void DictionaryGeneric() => _testCases.DictionaryGenericTest(ObservableDictionary);

    }
}
