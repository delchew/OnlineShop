using OnlineShopWebApp.ViewModels;
using OnlineShopWebApp.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Services
{
    public class Pagination : IPagination
    {
        public ProductsPageViewModel GetProductsPage(IEnumerable<ProductViewModel> productsList, int pageNumber, int productsOnPageCount)
        {
            var selectedProducts = productsList.Skip((pageNumber - 1) * productsOnPageCount).Take(productsOnPageCount);
            var page = new PageViewModel(productsList.Count(), pageNumber, productsOnPageCount);
            return new ProductsPageViewModel
            {
                Products = selectedProducts,
                Page = page
            };
        }
    }
}
