using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;

namespace OnlineShopWebApp.Views.Shared.Components.AddAndRemoveFromFavoritesButton
{
    public class AddAndRemoveFromFavoritesButtonViewComponent : ViewComponent
    {
        private readonly IFavoriteProductsRepository _favoriteRepository;

        public AddAndRemoveFromFavoritesButtonViewComponent(IFavoriteProductsRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public IViewComponentResult Invoke(ProductViewModel product)
        {
            var userId = HttpContext.User.Identity.Name ?? HttpContext.Request.Cookies[Constants.UnsignedUserId];
            ViewData["productIsFavorite"] = _favoriteRepository.Contains(userId, product.Id);
            return View("AddAndRemoveFromFavoritesButton", product);
        }
    }
}
