using Gstc.Collections.Observable.Standard;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {

    [TestFixture]
    public class ObservableListTestInterface {

        public ObservableList<TestItem> ObservableList;

        public InterfaceTestCases testCases = new InterfaceTestCases();

        [SetUp]
        public void TestInit() {
            testCases.TestInit();
            ObservableList = new ObservableList<TestItem>();
        }

        [Test, Description("Tests for compatibility with ICollection Interface")]
        public void CollectionInterface() {
            //Test needs 3 test items.
            ObservableList.Add(new TestItem()); 
            ObservableList.Add(new TestItem());
            ObservableList.Add(new TestItem());
            testCases.CollectionTest(ObservableList);
        }

        [Test, Description("Tests for compatibility with ICollection<> Interface")]
        public void CollectionGenericInterface() => testCases.CollectionGenericTest(ObservableList);

        [Test, Description("Tests usage of IList<> Interface")]
        public void ListInterface() => testCases.ListTest(ObservableList);
    }
}
