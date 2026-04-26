using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public enum PageType
    {
        Cars,
        Rentals,
        Customers
    }

    public enum DatabaseType
    {
        SqlServer,
        Sqlite
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarPageViewModel CarPage { get; }
        public CarEditViewModel CarEdit { get; }
        public RentalPageViewModel RentalPage { get; }
        public CustomerPageViewModel CustomerPage { get; }

        private PageType _currentPage;

        public object CurrentPageViewModel
        {
            get
            {
                return CurrentPage switch
                {
                    PageType.Cars => (object)CarPage,
                    PageType.Rentals => RentalPage,
                    PageType.Customers => CustomerPage,
                    _ => CarPage
                };
            }
        }

        public PageType CurrentPage
        {
            get => _currentPage;
            set
            {
                if (_currentPage != value)
                {
                    _currentPage = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CurrentPageViewModel));
                }
            }
        }

        private DatabaseType _currentDatabase;
        public DatabaseType CurrentDatabase
        {
            get => _currentDatabase;
            set
            {
                if (_currentDatabase != value)
                {
                    _currentDatabase = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand ShowCarsCommand { get; }
        public ICommand ShowRentalsCommand { get; }
        public ICommand ShowCustomersCommand { get; }

        public MainViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            CarPage = new CarPageViewModel(unitOfWork);
            RentalPage = new RentalPageViewModel(unitOfWork);
            CustomerPage = new CustomerPageViewModel(unitOfWork);

            CurrentPage = PageType.Cars;
            CarEdit = new CarEditViewModel(unitOfWork);
            CarEdit = new CarEditViewModel(unitOfWork);
            _ = CarEdit.LoadLocationsAsync();
            CurrentDatabase = DatabaseType.SqlServer;

            ShowCarsCommand = new RelayCommand(_ => CurrentPage = PageType.Cars);
            ShowRentalsCommand = new RelayCommand(_ => CurrentPage = PageType.Rentals);
            ShowCustomersCommand = new RelayCommand(_ => CurrentPage = PageType.Customers);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
