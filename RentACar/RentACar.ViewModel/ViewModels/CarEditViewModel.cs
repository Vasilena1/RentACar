using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RentACar.Model.Entities;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public class CarEditViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarEditViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
            Current = new Car
            {
                Status = "Available"
            };
        }

        public void StartEdit(Car car)
        {
            Current = car;
        }

        public async Task SaveAsync()
        {
            if (Current == null)
                return;

            // Временно: докато нямаме UI за Location, задаваме някакво валидно Id
            if (Current.LocationId == 0)
            {
                Current.LocationId = 1; // предполага се, че имаш Location с Id=1
            }

            if (Current.Id == 0)
            {
                await _unitOfWork.Cars.AddAsync(Current);
            }

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