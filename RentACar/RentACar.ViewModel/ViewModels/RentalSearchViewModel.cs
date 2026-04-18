using System;
using System.Threading.Tasks;
using RentACar.Model.Interfaces;

namespace RentACar.ViewModel.ViewModels
{
    public class RentalSearchViewModel
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly RentalListViewModel _rentalList;

        public RentalSearchViewModel(IUnitOfWork unitOfWork, RentalListViewModel rentalList)
        {
            _unitOfWork = unitOfWork;
            _rentalList = rentalList;

            Status = null; // showing all, by default
        }

        public string? Status { get; set; }

        public DateTime? PickupFrom { get; set; }

        public DateTime? PickupTo { get; set; }

        public DateTime? DropoffFrom { get; set; }

        public DateTime? DropoffTo { get; set; }

        public string? CustomerNameContains { get; set; }

        public int? PickupLocationId { get; set; }

        public int? DropoffLocationId { get; set; }
        public object UnitOfWork { get; set; }

        public async Task SearchAsync()
        {
            var rentals = await _unitOfWork.Rentals.SearchAsync(
                Status,
                PickupFrom,
                PickupTo,
                DropoffFrom,
                DropoffTo,
                CustomerNameContains,
                PickupLocationId,
                DropoffLocationId);

            _rentalList.SetRentals(rentals);
        }
    }
}
