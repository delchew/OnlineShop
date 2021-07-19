using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Services.Interfaces;

namespace OnlineShopWebApp.Views.Shared.Components.Favorites
{
    public class FavoritesCountViewComponent : ViewComponent
    {
        private readonly IFavoriteProductsRepository _favoriteRepository;

        public FavoritesCountViewComponent(IFavoriteProductsRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.Identity.Name ?? HttpContext.Request.Cookies[Constants.UnsignedUserId];
            if (userId != null)
            {
                return View("FavoritesCount", _favoriteRepository.GetProductsCount(userId));
            }
            return View("FavoritesCount", 0);
        }
    }
}
