using Gstc.Collections.Observable;
using Gstc.Collections.Observable.Nonstandard;

namespace Gstc.Collection.Observable.Examples {
    //Implementation of abstract class Observable list Adapter.
    public class CustomerViewModelList : ObservableListAdapter<Customer, CustomerViewModel> {
        public override CustomerViewModel Convert(Customer item) => new CustomerViewModel(item);
        public override Customer Convert(CustomerViewModel item) => item.Customer;
    }
}
