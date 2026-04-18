using RentACar.Model.Data;
using RentACar.Model.Interfaces;
using RentACar.Model.Repositories;

namespace RentACar.Model.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly RentACarDbContext _context;

        public UnitOfWork(RentACarDbContext context)
        {
            _context = context;

            Cars = new CarRepository(_context);
            Rentals = new RentalRepository(_context);
            Customers = new CustomerRepository(_context);
            Locations = new LocationRepository(_context);
        }

        public ICarRepository Cars { get; }

        public IRentalRepository Rentals { get; }

        public ICustomerRepository Customers { get; }

        public ILocationRepository Locations { get; }

        public Task<int> SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync()
        {
            await _context.DisposeAsync();
        }
    }
}
