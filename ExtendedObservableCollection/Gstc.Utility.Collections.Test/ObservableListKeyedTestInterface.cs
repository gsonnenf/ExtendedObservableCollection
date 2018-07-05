using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gstc.Collections.Observable.Nonstandard;
using Gstc.Collections.Observable.Standard;
using NUnit.Framework;

namespace Gstc.Collections.Observable.Test {
    [TestFixture]
    public class ObservableListKeyedTestInterface {
        public ObservableListKeyed<string,TestItem> ObservableList;

        private readonly InterfaceTestCases _testCases = new InterfaceTestCases();

        [SetUp]
        public void TestInit() {
            _testCases.TestInit();
            ObservableList = new ObservableListKeyedFunc<string,TestItem>( item => item.Id );
        }

        [Test, Description("Tests for compatibility with ICollection Interface")]
        public void CollectionInterface() {
            //Test needs 3 test items.
            ObservableList.Add(new TestItem());
            ObservableList.Add(new TestItem());
            ObservableList.Add(new TestItem());
            _testCases.CollectionTest(ObservableList);
        }

        [Test, Description("Tests for compatibility with ICollection<> Interface")]
        public void CollectionGenericInterface() => _testCases.CollectionGenericTest(ObservableList);

        [Test, Description("Tests usage of IList<> Interface")]
        public void ListInterface() => _testCases.ListTest(ObservableList);

        [Test, Description("Tests usage of Dictionary<> Interface")]
        public void DictionaryGenericInterface() => _testCases.DictionaryGenericTest(ObservableList);

    }
}
