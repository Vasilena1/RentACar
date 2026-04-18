using System.Collections.Generic;
using System.Threading.Tasks;
using RentACar.Model.Entities;

namespace RentACar.Model.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();

        Task<List<Car>> SearchAsync(
            string? category,
            string? status,
            decimal? dailyRateMin,
            decimal? dailyRateMax,
            string? makeContains,
            string? modelContains,
            int? locationId);

        Task<Car?> GetByIdAsync(int id);

        Task AddAsync(Car car);

        Task DeleteAsync(Car car);

    }
}
