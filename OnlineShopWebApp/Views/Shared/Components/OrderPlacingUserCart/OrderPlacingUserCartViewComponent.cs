using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;

namespace OnlineShopWebApp.Views.Shared.Components.OrderPlacingUserCart
{
    public class OrderPlacingUserCartViewComponent : ViewComponent
    {
        private readonly ICartsRepository _cartRepository;
        private readonly IMapper _mapper;

        public OrderPlacingUserCartViewComponent(ICartsRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = _mapper.Map<UserCartViewModel>(_cartRepository.Get(HttpContext.User.Identity.Name));
            return View("OrderPlacingUserCart", viewModel);
        }
    }
}
