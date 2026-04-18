namespace RentACar.Model.Interfaces
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        ICarRepository Cars { get; }

        IRentalRepository Rentals { get; }

        ICustomerRepository Customers { get; }

        ILocationRepository Locations { get; }

        Task<int> SaveChangesAsync();
    }
}
