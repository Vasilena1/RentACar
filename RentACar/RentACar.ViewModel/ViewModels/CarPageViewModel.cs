using RentACar.Model.Interfaces;
using RentACar.ViewModel.ViewModels;

namespace RentACar.ViewModel.ViewModels
{
    public class CarPageViewModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public CarPageViewModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            List = new CarListViewModel();
            Search = new CarSearchViewModel(_unitOfWork, List);
            Edit = new CarEditViewModel(_unitOfWork);
        }

        public CarSearchViewModel Search { get; }
        public CarListViewModel List { get; }
        public CarEditViewModel Edit { get; }

        public async Task LoadAsync()
        {
            var cars = await _unitOfWork.Cars.GetAllAsync();
            List.SetCars(cars);
        }
    }
}