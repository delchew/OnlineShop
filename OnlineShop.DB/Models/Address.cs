using System.Collections.Generic;

namespace OnlineShop.DB.Models
{
    public class Address
    {
        public int Id { get; set; }

        public string Locality { get; set; }

        public string Street { get; set; }

        public string BuildingNumber { get; set; }

        public string FlatOfficeNumber { get; set; }


        public List<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"{Locality}, {Street}, {BuildingNumber} - {FlatOfficeNumber}";
        }
    }
}
