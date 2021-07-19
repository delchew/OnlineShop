using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;
using System;

namespace OnlineShopWebApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IMapper _mapper;

        public ProductController(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }
        public IActionResult Index(Guid id)
        {
            return View(_mapper.Map<ProductViewModel>(_productsRepository.GetById(id)));
        }

        public IActionResult AddToFavorites(Guid Id)
        {
            return RedirectToAction("Index", Id);
        }
    }
}
