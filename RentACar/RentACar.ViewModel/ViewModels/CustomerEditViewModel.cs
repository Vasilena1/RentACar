using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using RentACar.Model.Entities;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public class CustomerEditViewModel : INotifyPropertyChanged
    {
        private readonly IUnitOfWork _unitOfWork;
        public bool HasCurrent => Current != null;

        public CustomerEditViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        private Customer? _current;
        public Customer? Current
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

        public void StartNew()
        {
            Current = new Customer();
        }

        public void StartEdit(Customer customer)
        {
            Current = customer;
        }

        public async Task SaveAsync()
        {
            if (Current == null)
                return;

            if (Current.Id == 0)
            {
                await _unitOfWork.Customers.AddAsync(Current);
            }

            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteAsync()
        {
            if (Current == null)
                return;

            if (Current.Id != 0)
            {
                await _unitOfWork.Customers.DeleteAsync(Current);
                await _unitOfWork.SaveChangesAsync();
            }

            Current = null;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
