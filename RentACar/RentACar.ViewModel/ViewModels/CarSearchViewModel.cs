using System.Threading.Tasks;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public class CarSearchViewModel
    {
        //collects the values from the filters, in case of search it calls Car.SearchAsync and passes those values, then it sets the result to CarListViewModel
        private readonly IUnitOfWork _unitOfWork;
        private readonly CarListViewModel _carList;

        public CarSearchViewModel(IUnitOfWork unitOfWork, CarListViewModel carList)
        {
            _unitOfWork = unitOfWork;
            _carList = carList;

            Status = "Available"; // default
        }

        public string? Category { get; set; }

        public string? Status { get; set; }

        public decimal? DailyRateMin { get; set; }

        public decimal? DailyRateMax { get; set; }

        public string? MakeContains { get; set; }

        public string? ModelContains { get; set; }

        public int? LocationId { get; set; }

        public async Task SearchAsync()
        {
            var cars = await _unitOfWork.Cars.SearchAsync(
                Category,
                Status,
                DailyRateMin,
                DailyRateMax,
                MakeContains,
                ModelContains,
                LocationId);

            _carList.SetCars(cars);
        }
    }
}
