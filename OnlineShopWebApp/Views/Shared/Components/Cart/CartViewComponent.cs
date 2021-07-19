using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Services.Interfaces;
using System.Linq;

namespace OnlineShopWebApp.Views.Shared.ViewComponents.CartViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private readonly ICartsRepository _cartRepository;
        private readonly IMapper _mapper;

        public CartViewComponent(ICartsRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var userId = HttpContext.User.Identity.Name ?? HttpContext.Request.Cookies[Constants.UnsignedUserId];
            if (userId != null)
            {
                var userCart = _cartRepository.Get(userId);
                return View("Cart", userCart?.CartItems?.Sum(c => c.Quantity) ?? 0);
            }
            return View("Cart", 0);
        }
    }
}
