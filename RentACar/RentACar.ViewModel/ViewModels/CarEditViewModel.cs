using RentACar.Model.Entities;
using RentACar.Model.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace RentACar.ViewModel.ViewModels
{
    public class CarEditViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        public ObservableCollection<Location> Locations { get; } = new();

        public CarEditViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Location? _selectedLocation;
        public Location? SelectedLocation
        {
            get => _selectedLocation;
            set
            {
                if (_selectedLocation != value)
                {
                    _selectedLocation = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task LoadLocationsAsync()
        {
            System.Diagnostics.Debug.WriteLine("Locations loaded: " + Locations.Count);
            Locations.Clear();
            var all = await _unitOfWork.Locations.GetAllAsync();

            foreach (var loc in all)
                Locations.Add(loc);
        }

        private Car? _current;
        public Car? Current
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

        public void StartNew()
        {
            Current = new Car { Status = "Available" };
            SelectedLocation = Locations.FirstOrDefault(); // по избор
        }

        public void StartEdit(Car car)
        {
            Current = car;
            SelectedLocation = Locations.FirstOrDefault(l => l.Id == car.LocationId);
        }

        public async Task SaveAsync()
        {
            if (Current == null)
                return;

            if (SelectedLocation != null)
                Current.LocationId = SelectedLocation.Id;

            if (Current.Id == 0)
                await _unitOfWork.Cars.AddAsync(Current);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync()
        {
            if (Current == null)
                return;

            if (Current.Id != 0)
            {
                await _unitOfWork.Cars.DeleteAsync(Current);
                await _unitOfWork.SaveChangesAsync();
            }

            Current = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}