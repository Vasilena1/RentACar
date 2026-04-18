using System.Collections.ObjectModel;
using RentACar.Model.Entities;

namespace RentACar.ViewModel.ViewModels
{
    public class CarListViewModel
    {
        // holds the list of cars that are displayed in the UI, and the selected car, it has a method to set the list of cars and a method to clear the list and the selected car
        // Cars is ObservableCollection because it needs to notify the UI when the list changes, SelectedCar is nullable because there might be no car selected
        // SetCars will be called by CarSearchViewModel when the search is done, Clear will be called when the user wants to clear the search results
        public ObservableCollection<Car> Cars { get; } = new ObservableCollection<Car>();

        private Car? _selectedCar;
        public Car? SelectedCar
        {
            get => _selectedCar;
            set => _selectedCar = value;
        }

        public void SetCars(IEnumerable<Car> cars)
        {
            Cars.Clear();
            foreach (var car in cars)
            {
                Cars.Add(car);
            }
        }

        public void Clear()
        {
            Cars.Clear();
            SelectedCar = null;
        }
    }
}
