using System;

namespace OnlineShop.DB.Models
{
    public class UserComparableProduct
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        
        public Guid ProductId { get; set; }


        public Product Product { get; set; }

    }
}
