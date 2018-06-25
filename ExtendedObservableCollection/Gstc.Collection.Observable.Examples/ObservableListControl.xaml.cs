using System.Windows.Controls;
using Gstc.Collections.Observable;
using Gstc.Collections.Observable.Standard;

namespace Gstc.Collection.Observable.Examples {
    /// <summary>
    /// Interaction logic for ObservableListControl.xaml
    /// </summary>
    public partial class ObservableListControl : UserControl {

        public ObservableList<Customer> CustomerObservableList { get; set; } = new ObservableList<Customer>();

        public ObservableListControl() {
            InitializeComponent();
            DataContext = this;

            CustomerObservableList.List = Customer.GenerateCustomerList();

            CustomerObservableList.CollectionChanged += (sender, args) => {
                string message = "\nCollection Changed:";
                             
                if (args.NewItems != null)
                    foreach (Customer customer in args.NewItems)
                        message += ("\nCustomer Added: " + customer.FirstName + " " + customer.LastName);

                if (args.OldItems != null)
                    foreach (Customer customer in args.OldItems)
                        message += ("\nCustomer Removed: " + customer.FirstName + " " + customer.LastName);

                EventTextBox.Text = message + "\n\n";
            };
        }
         
        private void Button_Click_AddRange(object sender, System.Windows.RoutedEventArgs e) {
            CustomerObservableList.AddRange(Customer.GenerateCustomerList());
        }

        private void Button_Click_Add(object sender, System.Windows.RoutedEventArgs e) {
            CustomerObservableList.Add(Customer.GenerateCustomer());
        }

        private void Button_Click_Remove(object sender, System.Windows.RoutedEventArgs e) {
            if (CustomerObservableList.Count > 0) CustomerObservableList.RemoveAt(0);
        }

        private void Button_Click_New(object sender, System.Windows.RoutedEventArgs e) {
            CustomerObservableList.List = Customer.GenerateCustomerList();
        }
    }
}
