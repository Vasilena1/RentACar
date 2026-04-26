using RentACar.Model.Data;
using RentACar.Model.Interfaces;
using RentACar.Model.UnitOfWork;

namespace RentACar.Blazor.Services
{
    /// <summary>
    /// Singleton — holds the current DB provider and rebuilds UnitOfWork on switch.
    /// </summary>
    public class DatabaseProviderService
    {
        private RentACarDbContext? _context;

        public string CurrentProvider { get; private set; } = "Sqlite";

        public IUnitOfWork UnitOfWork { get; private set; }

        public event Action? ProviderChanged;

        public DatabaseProviderService()
        {
            RebuildUnitOfWork();
        }

        public void SwitchTo(string provider)
        {
            if (provider == CurrentProvider) return;

            CurrentProvider = provider;
            RebuildUnitOfWork();
            ProviderChanged?.Invoke();
        }

        private void RebuildUnitOfWork()
        {
            _context?.Dispose();
            DbContextFactory.Provider = CurrentProvider;
            _context = DbContextFactory.Create();
            _context.Database.EnsureCreated();
            UnitOfWork = new UnitOfWork(_context);
        }
    }
}
