using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;
using OnlineShopWebApp.Services.Interfaces;
using System.Collections.Generic;

namespace OnlineShopWebApp.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductsRepository _productRepository;
        private readonly IPagination _pagination;
        private readonly IMapper _mapper;

        public SearchController(IProductsRepository productRepository, IPagination pagination, IMapper mapper)
        {
            _productRepository = productRepository;
            _pagination = pagination;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Index(string searchString, int pageNumber = 1)
        {
            ViewData["controllerName"] = "Search";
            ViewData["actioName"] = "Index";
            ViewData["searchString"] = searchString;
            var productsList = _mapper.Map<IEnumerable<ProductViewModel>>(_productRepository.SearchByBrandOrTitleMatch(searchString));
            return View(_pagination.GetProductsPage(productsList, pageNumber, Constants.ProductsOnPageCount));
        }
    }
}
