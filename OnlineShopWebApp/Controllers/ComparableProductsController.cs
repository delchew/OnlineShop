using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Controllers
{
    public class ComparableProductsController : BaseController
    {
        private readonly IProductsRepository _productsRepository;
        private readonly IComparableProductsRepository _comparableRepository;
        private readonly IMapper _mapper;


        public ComparableProductsController(IProductsRepository productsRepository, IComparableProductsRepository comparableRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _comparableRepository = comparableRepository;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<ProductViewModel>>(_comparableRepository.GetAll(GetUserId())));
        }
        public IActionResult AddToComparables(Guid id)
        {
            if(!_comparableRepository.Contains(GetUserId(), id))
            {
                var product = _productsRepository.GetById(id);
                _comparableRepository.Add(GetUserId(), product);
            }
            return RedirectToAction("Index", "Home", new { pageNumber = TempData["lastPageNumber"] });
        }

        public IActionResult RemoveFromComparables(Guid id)
        {
            if (_comparableRepository.Contains(GetUserId(), id))
            {
                _comparableRepository.Remove(GetUserId(), id);
            }
            return RedirectToAction("Index", "Home", new { pageNumber = TempData["lastPageNumber"] });
        }

        public IActionResult RemoveFromComparablesList(Guid id)
        {
            if (_comparableRepository.Contains(GetUserId(), id))
            {
                _comparableRepository.Remove(GetUserId(), id);
            }
            return RedirectToAction("Index");
        }

        public IActionResult ClearComparables()
        {
            _comparableRepository.Clear(GetUserId());
            return RedirectToAction("Index");
        }
    }
}
