using System.Windows;
using System.Windows.Controls;
using RentACar.ViewModel.ViewModels;

namespace RentACar.Wpf.Views
{
    public partial class RentalPageView : UserControl
    {
        public RentalPageView()
        {
            InitializeComponent();
            Loaded += RentalPageView_Loaded;
        }

        private async void RentalPageView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is RentalPageViewModel vm)
            {
                await vm.LoadAsync();
            }
        }

        private void NewRentalButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RentalPageViewModel vm)
            {
                vm.Edit.StartNew();
            }
        }

        private void EditSelectedButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RentalPageViewModel vm)
            {
                if (vm.List.SelectedRental == null)
                {
                    MessageBox.Show("Моля, изберете наем за редакция.");
                    return;
                }

                vm.Edit.StartEdit(vm.List.SelectedRental);
            }
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RentalPageViewModel vm)
            {
                await vm.Edit.SaveAsync();
                await vm.LoadAsync();
            }
        }

        private async void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is RentalPageViewModel vm)
            {
                await vm.Search.SearchAsync();
            }
        }
    }
}