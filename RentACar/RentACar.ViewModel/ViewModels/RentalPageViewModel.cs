using System.Threading.Tasks;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public class RentalPageViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IUnitOfWork UnitOfWork { get; }

        public RentalPageViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            UnitOfWork = unitOfWork;

            List = new RentalListViewModel();
            Search = new RentalSearchViewModel(_unitOfWork, List);
            Edit = new RentalEditViewModel(_unitOfWork);
        }

        public RentalSearchViewModel Search { get; }
        public RentalListViewModel List { get; }
        public RentalEditViewModel Edit { get; }

        public async Task LoadAsync()
        {
            var rentals = await _unitOfWork.Rentals.GetAllAsync();
            List.SetRentals(rentals);

            await Edit.LoadLookupsAsync();
        }
    }
}