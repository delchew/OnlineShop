using App.Metrics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    [Authorize]
    public class OrderPlacingController : Controller
    {
        private readonly ICartsRepository _cartRepository;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        private readonly IMetrics _metrics;

        public OrderPlacingController(ICartsRepository cartRepository,
            IOrdersRepository ordersRepository, IMetrics metrics, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _ordersRepository = ordersRepository;
            _metrics = metrics;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult PlaceOrder(OrderViewModel orderViewModel)
        {
            if (orderViewModel.DeliveryType == DeliveryType.PickUp)
            {
                orderViewModel.Address = null;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(orderViewModel.Address.Locality) ||
                    string.IsNullOrWhiteSpace(orderViewModel.Address.Street) ||
                    string.IsNullOrWhiteSpace(orderViewModel.Address.BuildingNumber) ||
                    string.IsNullOrWhiteSpace(orderViewModel.Address.FlatOfficeNumber))
                {
                    ModelState.AddModelError("unvalidAddress", "Заполните все поля адреса для доставки!");
                }
            }

            if (ModelState.IsValid)
            {
                var order = _mapper.Map<Order>(orderViewModel);
                order.UserId = HttpContext.User.Identity.Name;
                order.CartItems = _cartRepository.Get(HttpContext.User.Identity.Name).CartItems;
                _ordersRepository.Add(order);
                _cartRepository.RemoveCart(HttpContext.User.Identity.Name);
                return View();
            }

            return View("Index", orderViewModel);
        }
    }
}
