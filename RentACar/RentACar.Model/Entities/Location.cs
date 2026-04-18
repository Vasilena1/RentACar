    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Model.Entities
{
    public class Location
    {
        public int Id { get; set; }

        // Example: "Sofia - Airport (SOF)"
        public string Name { get; set; } = string.Empty;

        public string City { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public ICollection<Car> Cars { get; set; } = new List<Car>();

        public ICollection<Rental> Pickups { get; set; } = new List<Rental>();

        public ICollection<Rental> Dropoffs { get; set; } = new List<Rental>();
    }
}
