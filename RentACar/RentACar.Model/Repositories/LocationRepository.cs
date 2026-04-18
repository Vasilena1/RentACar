using Microsoft.EntityFrameworkCore;
using RentACar.Model.Data;
using RentACar.Model.Entities;
using RentACar.Model.Interfaces;

namespace RentACar.Model.Repositories
{
    public class LocationRepository : ILocationRepository
    {
        private readonly RentACarDbContext _context;

        public LocationRepository(RentACarDbContext context)
        {
            _context = context;
        }

        public Task<List<Location>> GetAllAsync()
        {
            return _context.Locations
                .OrderBy(l => l.City)
                .ThenBy(l => l.Name)
                .ToListAsync();
        }

        public Task<Location?> GetByIdAsync(int id)
        {
            return _context.Locations
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(Location location)
        {
            await _context.Locations.AddAsync(location);
        }
    }
}
