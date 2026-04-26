using Microsoft.EntityFrameworkCore;

namespace RentACar.Model.Data
{
    public static class DbContextFactory
    {
        public static string Provider { get; set; } = "Sqlite";

        public static RentACarDbContext Create()
        {
            // ВАЖНО: generic версиия
            var optionsBuilder = new DbContextOptionsBuilder<RentACarDbContext>();

            if (Provider == "SqlServer")
            {
                var cs = "Server=(localdb)\\MSSQLLocalDB;Database=RentACarDb;Trusted_Connection=True;MultipleActiveResultSets=True;";
                optionsBuilder.UseSqlServer(cs);
            }
            else // Sqlite
            {
                var dbPath = DbPathHelper.GetSharedSqlitePath();
                var cs = $"Data Source={dbPath}";
                optionsBuilder.UseSqlite(cs);
            }

            return new RentACarDbContext(optionsBuilder.Options);
        }
    }
}