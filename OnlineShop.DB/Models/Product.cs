using System;
using System.Collections.Generic;

namespace OnlineShop.DB.Models
{
    public class Product
    {
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Title { get; set; }

        public string Color { get; set; }

        public int ModelYear { get; set; }

        public string Country { get; set; }

        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public List<ProductImage> ProductImages { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
