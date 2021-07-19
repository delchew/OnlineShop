using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.Extensions;
using OnlineShopWebApp.ViewModels;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class CartController : BaseController
    {
        private readonly IProductsRepository _productsRepository;
        private readonly ICartsRepository _cartRepository;
        private readonly IMapper _mapper;


        public CartController(IProductsRepository productsRepository, ICartsRepository cart, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _cartRepository = cart;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            var userCart = _cartRepository.Get(GetUserId());
            var model = _mapper.Map<UserCartViewModel>(userCart);
            return View(model);
        }

        public IActionResult AddToCart(Guid id)
        {
            var product = _productsRepository.GetById(id);
            
            _cartRepository.Add(GetUserId(), product);
            return RedirectToAction("Index");
        }

        public IActionResult RemoveFromCart(Guid id)
        {
            var product = _productsRepository.GetById(id);
            _cartRepository.Remove(GetUserId(), product);
            return RedirectToAction("Index");
        }

        public IActionResult ClearCart()
        {
            _cartRepository.Clear(GetUserId());
            return RedirectToAction("Index");
        }
    }
}
