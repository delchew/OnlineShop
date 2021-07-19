using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.ViewModels
{
    public class UserCartViewModel
    {
        public int Id { get; set; }

        public UserViewModel User { get; set; }

        public string UserId { get; set; }

        public List<CartItemViewModel> CartItems { get; set; }

        public int ProductsCount => CartItems.Sum(c => c.Quantity);

        public decimal TotalCost => CartItems.Sum(p => p.Product.Cost * p.Quantity);
    }
}
