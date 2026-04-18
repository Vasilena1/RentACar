using Microsoft.EntityFrameworkCore;
using RentACar.Model.Data;
using RentACar.Model.Entities;
using RentACar.Model.Interfaces;

namespace RentACar.Model.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly RentACarDbContext _context;

        public CustomerRepository(RentACarDbContext context)
        {
            _context = context;
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return _context.Customers
                .OrderBy(c => c.FullName)
                .ToListAsync();
        }

        public Task<Customer?> GetByIdAsync(int id)
        {
            return _context.Customers
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddAsync(Customer customer)
        {
            await _context.Customers.AddAsync(customer);
        }

        public Task DeleteAsync(Customer customer)
        {
            _context.Customers.Remove(customer);
            return Task.CompletedTask;
        }

    }
}
