using System;

namespace OnlineShopWebApp.ViewModels
{
    public class ProductImageViewModel
    {
        public Guid Id { get; set; }

        public string ImageUrl { get; set; }

        public Guid ProductId { get; set; }
    }
}
