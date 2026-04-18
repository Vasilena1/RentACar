using RentACar.Model.Entities;

namespace RentACar.Model.Interfaces
{
    public interface ILocationRepository
    {
        Task<List<Location>> GetAllAsync();

        Task<Location?> GetByIdAsync(int id);

        Task AddAsync(Location location);
    }
}
