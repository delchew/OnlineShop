using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;

namespace OnlineShopWebApp.Views.Shared.Components.HeaderAvatar
{
    public class HeaderAvatarViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public HeaderAvatarViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userName = HttpContext.User.Identity.Name;
            if (userName != null)
            {
                var user = _userManager.FindByNameAsync(userName).Result;
                return View("HeaderAvatar", user.AvatarImageUrl);
            }
            return View("HeaderAvatar", string.Empty);
        }
    }
}
