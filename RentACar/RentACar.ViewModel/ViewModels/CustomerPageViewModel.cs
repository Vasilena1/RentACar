using System.Threading.Tasks;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public class CustomerPageViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerPageViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            List = new CustomerListViewModel();
            Edit = new CustomerEditViewModel(unitOfWork);
        }

        public CustomerListViewModel List { get; }
        public CustomerEditViewModel Edit { get; }

        public async Task LoadAsync()
        {
            var customers = await _unitOfWork.Customers.GetAllAsync();
            List.SetCustomers(customers);
        }

    }
}
