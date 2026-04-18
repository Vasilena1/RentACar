using System.Collections.Generic;
using System.Collections.ObjectModel;
using RentACar.Model.Entities;

namespace RentACar.ViewModel.ViewModels
{
    public class RentalListViewModel
    {
        public ObservableCollection<Rental> Rentals { get; } = new ObservableCollection<Rental>();

        private Rental? _selectedRental;
        public Rental? SelectedRental
        {
            get => _selectedRental;
            set => _selectedRental = value;
        }

        public void SetRentals(IEnumerable<Rental> rentals)
        {
            Rentals.Clear();
            foreach (var rental in rentals)
            {
                Rentals.Add(rental);
            }
        }

        public void Clear()
        {
            Rentals.Clear();
            SelectedRental = null;
        }
    }
}
