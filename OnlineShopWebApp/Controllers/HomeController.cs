using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;
using OnlineShopWebApp.Services.Interfaces;
using System.Diagnostics;
using System.Linq;
using OnlineShop.DB;
using AutoMapper;
using OnlineShop.DB.Models;
using System.Collections.Generic;
using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Histogram;

namespace OnlineShopWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductsRepository _productRepository;
        private readonly IPagination _pagination;
        private readonly IMapper _mapper;
        private readonly IMetrics _metrics;

        public HomeController(IProductsRepository productRepository, IPagination pagination, IMapper mapper, IMetrics metrics)
        {
            _productRepository = productRepository;
            _pagination = pagination;
            _mapper = mapper;
            _metrics = metrics;
        }
        public IActionResult Index(int pageNumber = 1)
        {
            var products = _productRepository.GetAll()
                                             .Where(p => !p.IsDeleted)
                                             .ToList();
            var models = _mapper.Map<IEnumerable<ProductViewModel>>(products);
            _metrics.Measure.Counter.Increment(MeasureOptions.MainPageRequestCounterOptions);

            ViewData["controllerName"] = "Home";
            ViewData["actioName"] = "Index";
            return View(_pagination.GetProductsPage(models, pageNumber, Constants.ProductsOnPageCount));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
