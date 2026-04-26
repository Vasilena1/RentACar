using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Model.Entities
{
    public class Car
    {
        public int Id { get; set; }

        public string RegistrationNumber { get; set; } = string.Empty;

        public string Make { get; set; } = string.Empty;

        public string Model { get; set; } = string.Empty;

        // Examples: "Economy", "Compact", "SUV", "Electric" ...
        public string Category { get; set; } = string.Empty;

        public decimal DailyRate { get; set; }

        // Examples: "Available", "Rented", "InService"
        public string Status { get; set; } = "Available";

        public int? LocationId { get; set; }

        public Location? Location { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
