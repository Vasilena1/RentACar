using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RentACar.Model.Entities;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public class RentalEditViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        public RentalEditViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Rental? _current;
        public Rental? Current
        {
            get => _current;
            private set
            {
                if (_current != value)
                {
                    _current = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(HasCurrent));
                }
            }
        }

        public bool HasCurrent => Current != null;

        public ObservableCollection<Car> Cars { get; } = new ObservableCollection<Car>();
        public ObservableCollection<Customer> Customers { get; } = new ObservableCollection<Customer>();

        private Car? _selectedCar;
        public Car? SelectedCar
        {
            get => _selectedCar;
            set
            {
                if (_selectedCar != value)
                {
                    _selectedCar = value;
                    OnPropertyChanged();

                    if (Current != null && _selectedCar != null)
                    {
                        Current.CarId = _selectedCar.Id;
                        Current.Car = null; // важно: не закачаме навигацията
                    }
                }
            }
        }

        private Customer? _selectedCustomer;
        public Customer? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    OnPropertyChanged();

                    if (Current != null && _selectedCustomer != null)
                    {
                        Current.CustomerId = _selectedCustomer.Id;
                        Current.Customer = null; // важно: не закачаме навигацията
                    }
                }
            }
        }

        public void StartNew()
        {
            Current = new Rental
            {
                PickupDateTime = DateTime.Now,
                DropoffDateTime = DateTime.Now.AddDays(1),
                Status = "Planned"
            };

            SelectedCar = null;
            SelectedCustomer = null;
        }

        public void StartEdit(Rental rental)
        {
            Current = rental;

            // показваме избраните в ComboBox-а
            SelectedCar = rental.Car;
            SelectedCustomer = rental.Customer;
        }

        public async Task SaveAsync()
        {
            if (Current == null)
                return;

            if (SelectedCar == null || SelectedCustomer == null)
                return;

            // само Id-та за кола и клиент
            Current.Car = null;
            Current.Customer = null;

            // ВРЕМЕННО: избягваме FK към Location, докато нямаме UI за локации
            if (Current.PickupLocationId == 0)
                Current.PickupLocationId = 1;
            if (Current.DropoffLocationId == 0)
                Current.DropoffLocationId = 1;

            if (Current.Id == 0)
            {
                await _unitOfWork.Rentals.AddAsync(Current);
            }

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task LoadLookupsAsync()
        {
            Cars.Clear();
            Customers.Clear();

            var cars = await _unitOfWork.Cars.GetAllAsync();
            foreach (var car in cars)
                Cars.Add(car);

            var customers = await _unitOfWork.Customers.GetAllAsync();
            foreach (var c in customers)
                Customers.Add(c);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}