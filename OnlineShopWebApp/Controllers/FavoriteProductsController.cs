using AutoMapper;
using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Controllers
{
    public class FavoriteProductsController : BaseController
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IFavoriteProductsRepository _favoriteRepository;
        private readonly IMetrics _metrics;
        private readonly IMapper _mapper;


        public FavoriteProductsController(IProductsRepository productsRepository, IFavoriteProductsRepository favoriteRepository, IMapper mapper, IMetrics metrics)
        {
            _productsRepository = productsRepository;
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
            _metrics = metrics;
        }
        public IActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(_favoriteRepository.GetAll(GetUserId())));
        }
        public IActionResult AddToFavorites(Guid id)
        {
            if(!_favoriteRepository.Contains(GetUserId(), id))
            {
                var product = _productsRepository.GetById(id);
                _favoriteRepository.Add(GetUserId(), product);
                var favoritesCount = _favoriteRepository.GetAllFavoritesCount();
                _metrics.Measure.Histogram.Update(MeasureOptions.ProductsInFavoritesHistogramOptions, favoritesCount);
            }
            return RedirectToAction("Index", "Home", new { pageNumber = TempData["lastPageNumber"] });
        }


        public IActionResult RemoveFromFavorites(Guid id)
        {
            if (_favoriteRepository.Contains(GetUserId(), id))
            {
                _favoriteRepository.Remove(GetUserId(), id);
                var favoritesCount = _favoriteRepository.GetAllFavoritesCount();
                _metrics.Measure.Histogram.Update(MeasureOptions.ProductsInFavoritesHistogramOptions, favoritesCount);
            }
            return RedirectToAction("Index", "Home", new { pageNumber = TempData["lastPageNumber"] });
        }

        public IActionResult RemoveFromFavoritesList(Guid id)
        {
            if (_favoriteRepository.Contains(GetUserId(), id))
            {
                _favoriteRepository.Remove(GetUserId(), id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearFavorites()
        {
            _favoriteRepository.Clear(GetUserId());
            return RedirectToAction("Index");
        }
    }
}
