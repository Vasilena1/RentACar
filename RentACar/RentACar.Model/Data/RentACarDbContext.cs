using Microsoft.EntityFrameworkCore;
using RentACar.Model.Entities;

namespace RentACar.Model.Data
{
    public class RentACarDbContext : DbContext
    {
        public RentACarDbContext(DbContextOptions<RentACarDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Customer> Customers { get; set; }

        public DbSet<Location> Locations { get; set; }

        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Car
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Location)
                .WithMany(l => l.Cars)
                .HasForeignKey(c => c.LocationId);

            // Rental -> Car
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarId);

            // Rental -> Customer
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Customer)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CustomerId);

            // Rental -> PickupLocation
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.PickupLocation)
                .WithMany(l => l.Pickups)
                .HasForeignKey(r => r.PickupLocationId)
                .OnDelete(DeleteBehavior.Restrict);

            // Rental -> DropoffLocation
            modelBuilder.Entity<Rental>()
                .HasOne(r => r.DropoffLocation)
                .WithMany(l => l.Dropoffs)
                .HasForeignKey(r => r.DropoffLocationId)
                .OnDelete(DeleteBehavior.Restrict);
            
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Sofia - Airport (SOF)" },
                new Location { Id = 2, Name = "Sofia - Center" },
                new Location { Id = 3, Name = "Plovdiv" },
                new Location { Id = 4, Name = "Varna" }
            );
        }
    }
}
