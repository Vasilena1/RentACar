using Microsoft.EntityFrameworkCore;
using RentACar.Model.Data;
using RentACar.Model.Entities;
using RentACar.Model.Interfaces;

namespace RentACar.Model.Repositories
{
    public class CarRepository : ICarRepository
    {
        private readonly RentACarDbContext _context;

        public CarRepository(RentACarDbContext context)
        {
            _context = context;
        }

        public async Task<List<Car>> SearchAsync(
            string? category,
            string? status,
            decimal? dailyRateMin,
            decimal? dailyRateMax,
            string? makeContains,
            string? modelContains,
            int? locationId)
        {
            IQueryable<Car> query = _context.Cars.Include(c => c.Location);

            if (!string.IsNullOrWhiteSpace(category))
                query = query.Where(c => c.Category == category);

            if (!string.IsNullOrWhiteSpace(status))
                query = query.Where(c => c.Status == status);

            if (dailyRateMin.HasValue)
                query = query.Where(c => c.DailyRate >= dailyRateMin.Value);

            if (dailyRateMax.HasValue)
                query = query.Where(c => c.DailyRate <= dailyRateMax.Value);

            if (!string.IsNullOrWhiteSpace(makeContains))
                query = query.Where(c => c.Make.Contains(makeContains));

            if (!string.IsNullOrWhiteSpace(modelContains))
                query = query.Where(c => c.Model.Contains(modelContains));

            if (locationId.HasValue)
                query = query.Where(c => c.LocationId == locationId.Value);

            return await query.ToListAsync();
        }

        public Task<Car?> GetByIdAsync(int id) =>
            _context.Cars
                .Include(c => c.Location)
                .FirstOrDefaultAsync(c => c.Id == id);

        public async Task AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
        }

        public Task<IEnumerable<Car>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<Car>>(_context.Cars.AsNoTracking().ToList());
        }

        public Task DeleteAsync(Car car)
        {
            _context.Cars.Remove(car);
            return Task.CompletedTask;
        }
    }
}
