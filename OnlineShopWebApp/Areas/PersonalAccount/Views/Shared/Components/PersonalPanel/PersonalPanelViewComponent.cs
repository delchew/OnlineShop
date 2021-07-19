using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;

namespace OnlineShopWebApp.Areas.PersonalAccount.Views.Shared.Components.PersonalPanel
{
    public class PersonalPanelViewComponent : ViewComponent
    {
        private readonly UserManager<User> _userManager;

        public PersonalPanelViewComponent(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public IViewComponentResult Invoke()
        {
            var userName = HttpContext.User.Identity.Name;
            if (userName != null)
            {
                var user = _userManager.FindByNameAsync(userName).Result;
                return View("PersonalPanel", user.AvatarImageUrl);
            }
            return View("PersonalPanel", string.Empty);
        }
    }
}
