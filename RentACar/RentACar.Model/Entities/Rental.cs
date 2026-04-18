using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Model.Entities
{
    public class Rental
    {
        public int Id { get; set; }

        public int CarId { get; set; }

        public Car? Car { get; set; }

        public int CustomerId { get; set; }

        public Customer? Customer { get; set; }

        public int PickupLocationId { get; set; }

        public Location? PickupLocation { get; set; }

        public int DropoffLocationId { get; set; }

        public Location? DropoffLocation { get; set; }

        public DateTime PickupDateTime { get; set; }

        public DateTime DropoffDateTime { get; set; }

        public decimal TotalPrice { get; set; }

        // Examples: "Active", "Finished", "Late", "Canceled"
        public string Status { get; set; } = "Active";
    }
}
