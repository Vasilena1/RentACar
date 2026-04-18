using System.Collections.Generic;
using System.Collections.ObjectModel;
using RentACar.Model.Entities;

namespace RentACar.ViewModel.ViewModels
{
    public class CustomerListViewModel
    {
        public ObservableCollection<Customer> Customers { get; } = new ObservableCollection<Customer>();

        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set => _selectedCustomer = value;
        }

        public void SetCustomers(IEnumerable<Customer> customers)
        {
            Customers.Clear();
            foreach (var customer in customers)
            {
                Customers.Add(customer);
            }
        }

        public void Clear()
        {
            Customers.Clear();
            SelectedCustomer = null;
        }
    }
}
