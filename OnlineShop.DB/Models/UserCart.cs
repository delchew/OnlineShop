using System.Collections.Generic;

namespace OnlineShop.DB.Models
{
    public class UserCart
    {
        public int Id { get; set; }

        public string UserId { get; set; }


        public List<CartItem> CartItems { get; set; }

    }
}
