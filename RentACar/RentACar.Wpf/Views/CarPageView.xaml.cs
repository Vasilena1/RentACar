using RentACar.ViewModel.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RentACar.Wpf.Views
{
    /// <summary>
    /// Interaction logic for CarPageView.xaml
    /// When this view is loaded, it will set its DataContext to the CarPage property of the MainViewModel.
    /// The button works directly with the Search command of the CarPageViewModel, which is responsible for executing the search logic and updating the Cars collection based on the search criteria. The UI will automatically reflect these changes due to data binding.
    /// </summary>
    public partial class CarPageView : UserControl
    {
        public CarPageView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                if (DataContext is MainViewModel mainVm)
                {
                    DataContext = mainVm.CarPage;
                }
            };
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CarPageViewModel vm)
            {
                await vm.Search.SearchAsync();
            }
        }
        private void NewCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CarPageViewModel vm)
            {
                vm.Edit.StartNew();
            }
        }

        private void EditSelectedCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CarPageViewModel vm)
            {
                if (vm.List.SelectedCar == null)
                {
                    MessageBox.Show("Моля, изберете кола за редакция.");
                    return;
                }

                vm.Edit.StartEdit(vm.List.SelectedCar);
            }
        }

        private async void SaveCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CarPageViewModel vm)
            {
                await vm.Edit.SaveAsync();
                await vm.LoadAsync();
            }
        }

        private async void DeleteCarButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is CarPageViewModel vm)
            {
                if (vm.List.SelectedCar == null)
                {
                    MessageBox.Show("Please, choose a client to be deleted.");
                    return;
                }

                var result = MessageBox.Show(
                    $"Do you really want to delete car - \"{vm.List.SelectedCar.RegistrationNumber}\"?",
                    "Confirm delete",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);

                if (result != MessageBoxResult.Yes)
                    return;

                vm.Edit.StartEdit(vm.List.SelectedCar);
                await vm.Edit.DeleteAsync();
                await vm.LoadAsync();
            }
        }


    }
}
