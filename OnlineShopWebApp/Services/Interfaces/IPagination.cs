using OnlineShopWebApp.ViewModels;
using System.Collections.Generic;

namespace OnlineShopWebApp.Services.Interfaces
{
    public interface IPagination
    {
        public ProductsPageViewModel GetProductsPage(IEnumerable<ProductViewModel> productsList, int pageNumber, int productsOnPageCount);
    }
}
