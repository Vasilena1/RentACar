using System.Windows;
using System.Windows.Controls;
using RentACar.ViewModel.ViewModels;

namespace RentACar.Wpf.Views
{
    public partial class CustomerPageView : UserControl
    {
        public CustomerPageView()
        {
            InitializeComponent();
            // DataContext идва от MainWindow (ContentControl -> CurrentPageViewModel)
        }

        private void NewCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as CustomerPageViewModel;
            if (vm == null)
            {
                MessageBox.Show("DataContext не е CustomerPageViewModel");
                return;
            }

            vm.Edit.StartNew();
        }

        private void EditSelectedCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as CustomerPageViewModel;
            if (vm == null)
            {
                MessageBox.Show("DataContext не е CustomerPageViewModel");
                return;
            }

            if (vm.List.SelectedCustomer != null)
            {
                vm.Edit.StartEdit(vm.List.SelectedCustomer);
            }
        }

        private async void SaveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as CustomerPageViewModel;
            if (vm == null)
            {
                MessageBox.Show("DataContext не е CustomerPageViewModel");
                return;
            }

            await vm.Edit.SaveAsync();
            await vm.LoadAsync();
        }

        private async void DeleteCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CustomerPageViewModel vm)
            {
                if (vm.List.SelectedCustomer == null)
                {
                    MessageBox.Show("Please, choose a client to be deleted.");
                    return;
                }

                var result = MessageBox.Show(
                    $"Do you really want to delete client - \"{vm.List.SelectedCustomer.FullName}\"?",
                    "Confirm delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result != MessageBoxResult.Yes)
                    return;

                vm.Edit.StartEdit(vm.List.SelectedCustomer);
                await vm.Edit.DeleteAsync();
                await vm.LoadAsync();
            }
        }

    }
}
