using RentACar.Model.Data;
using RentACar.Model.Interfaces;
using RentACar.Model.UnitOfWork;
using RentACar.ViewModel.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace RentACar.Wpf
{
    public partial class MainWindow : Window
    {
        private RentACarDbContext? _currentContext;
        private IUnitOfWork _unitOfWork;
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            DbContextFactory.Provider = "Sqlite"; // стартово
            InitWithCurrentProvider();

            Loaded += async (_, __) =>
            {
                await _mainViewModel.CarPage.Search.SearchAsync();
                await _mainViewModel.CustomerPage.LoadAsync();
            };
        }

        private void InitWithCurrentProvider()
        {
            _currentContext?.Dispose(); // затваря старите връзки

            _currentContext = DbContextFactory.Create();
            _currentContext.Database.EnsureCreated();

            _unitOfWork = new UnitOfWork(_currentContext);
            _mainViewModel = new MainViewModel(_unitOfWork);
            DataContext = _mainViewModel;
        }

        private async void SwitchDbButton_Click(object sender, RoutedEventArgs e)
        {
            if (DbContextFactory.Provider == "Sqlite")
                DbContextFactory.Provider = "SqlServer";
            else
                DbContextFactory.Provider = "Sqlite";

            InitWithCurrentProvider();

            await _mainViewModel.CarPage.Search.SearchAsync();
            await _mainViewModel.CustomerPage.LoadAsync();

            MessageBox.Show($"Database provider switched to: {DbContextFactory.Provider}");
        }

        private async void DbProviderCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsLoaded) return;

            if (DbProviderCombo.SelectedItem is ComboBoxItem item &&
                item.Tag is string providerKey)
            {
                DbContextFactory.Provider = providerKey;

                InitWithCurrentProvider();

                await _mainViewModel.CarPage.Search.SearchAsync();
                await _mainViewModel.CustomerPage.LoadAsync();

                MessageBox.Show($"Database provider switched to: {DbContextFactory.Provider}");
            }
        }
    }
}