using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;

namespace OnlineShopWebApp.Views.Shared.Components.AddAndRemoveFromComparesButton
{
    public class AddAndRemoveFromComparesButtonViewComponent : ViewComponent
    {
        private readonly IComparableProductsRepository _comparableRepository;

        public AddAndRemoveFromComparesButtonViewComponent(IComparableProductsRepository comparableRepository)
        {
            _comparableRepository = comparableRepository;
        }

        public IViewComponentResult Invoke(ProductViewModel product)
        {
            var userId = HttpContext.User.Identity.Name ?? HttpContext.Request.Cookies[Constants.UnsignedUserId];
            ViewData["productIsComparable"] = _comparableRepository.Contains(userId, product.Id);
            return View("AddAndRemoveFromComparesButton", product);
        }
    }
}
