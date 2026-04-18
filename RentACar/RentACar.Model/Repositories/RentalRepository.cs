using Microsoft.EntityFrameworkCore;
using RentACar.Model.Data;
using RentACar.Model.Entities;
using RentACar.Model.Interfaces;

namespace RentACar.Model.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly RentACarDbContext _context;

        public RentalRepository(RentACarDbContext context)
        {
            _context = context;
        }

        public async Task<List<Rental>> SearchAsync(
            string? status,
            DateTime? pickupFrom,
            DateTime? pickupTo,
            DateTime? dropoffFrom,
            DateTime? dropoffTo,
            string? customerNameContains,
            int? pickupLocationId,
            int? dropoffLocationId)
        {
            IQueryable<Rental> query = _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Include(r => r.PickupLocation)
                .Include(r => r.DropoffLocation);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(r => r.Status == status);

            if (pickupFrom.HasValue)
                query = query.Where(r => r.PickupDateTime >= pickupFrom.Value);

            if (pickupTo.HasValue)
                query = query.Where(r => r.PickupDateTime <= pickupTo.Value);

            if (dropoffFrom.HasValue)
                query = query.Where(r => r.DropoffDateTime >= dropoffFrom.Value);

            if (dropoffTo.HasValue)
                query = query.Where(r => r.DropoffDateTime <= dropoffTo.Value);

            if (!string.IsNullOrWhiteSpace(customerNameContains))
                query = query.Where(r => r.Customer.FullName.Contains(customerNameContains));

            if (pickupLocationId.HasValue)
                query = query.Where(r => r.PickupLocationId == pickupLocationId.Value);

            if (dropoffLocationId.HasValue)
                query = query.Where(r => r.DropoffLocationId == dropoffLocationId.Value);

            return await query.ToListAsync();
        }

        public Task<List<Rental>> GetAllAsync()
        {
            return _context.Rentals
                .OrderBy(r => r.PickupDateTime)
                .ToListAsync();
        }

        public Task<Rental?> GetByIdAsync(int id) =>
            _context.Rentals
                .Include(r => r.Car)
                .Include(r => r.Customer)
                .Include(r => r.PickupLocation)
                .Include(r => r.DropoffLocation)
                .FirstOrDefaultAsync(r => r.Id == id);

        public async Task AddAsync(Rental rental)
        {
            await _context.Rentals.AddAsync(rental);
        }
    }
}
