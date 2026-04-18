using RentACar.Model.Entities;

namespace RentACar.Model.Interfaces
{
    public interface IRentalRepository
    {
        Task<List<Rental>> GetAllAsync();

        Task<List<Rental>> SearchAsync(
            string? status,
            DateTime? pickupFrom,
            DateTime? pickupTo,
            DateTime? dropoffFrom,
            DateTime? dropoffTo,
            string? customerNameContains,
            int? pickupLocationId,
            int? dropoffLocationId);

        Task<Rental?> GetByIdAsync(int id);

        Task AddAsync(Rental rental);
    }
}
