using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Services.Interfaces;

namespace OnlineShopWebApp.Views.Shared.Components.Compare
{
    public class ComparesCountViewComponent : ViewComponent
    {
        private readonly IComparableProductsRepository _comparableRepository;

        public ComparesCountViewComponent(IComparableProductsRepository comparableRepository)
        {
            _comparableRepository = comparableRepository;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.Identity.Name ?? HttpContext.Request.Cookies[Constants.UnsignedUserId];
            if (userId != null)
            {
                return View("ComparesCount", _comparableRepository.GetProductsCount(userId));
            }
            return View("ComparesCount", 0);
        }
    }
}
