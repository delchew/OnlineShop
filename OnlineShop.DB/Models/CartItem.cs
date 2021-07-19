using System;

namespace OnlineShop.DB.Models
{
    public class CartItem
    {
        public int Id { get; set; }

        public int Quantity { get; set; }


        public Product Product { get; set; }

        public UserCart UserCart { get; set; }

        public Order Order { get; set; }

    }
}
