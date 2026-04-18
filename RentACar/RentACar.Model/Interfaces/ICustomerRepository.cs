using RentACar.Model.Entities;

namespace RentACar.Model.Interfaces
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllAsync();

        Task<Customer?> GetByIdAsync(int id);

        Task AddAsync(Customer customer);
        Task DeleteAsync(Customer customer);
    }
}
