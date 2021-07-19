using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;
using OnlineShopWebApp.Services.Interfaces;
using System;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using OnlineShopWebApp.Extensions;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]

    public class ProductsController : Controller
    {
        private readonly string _productsImagesPath = "\\pictures\\products\\";
        private readonly IProductsRepository _productRepository;
        private readonly IPagination _pagination;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMapper _mapper;


        public ProductsController(IProductsRepository productRepository, IPagination pagination,
            IWebHostEnvironment appEnvironment, IMapper mapper)
        {
            _productRepository = productRepository;
            _pagination = pagination;
            _appEnvironment = appEnvironment;
            _mapper = mapper;
        }

        public IActionResult Index(int pageNumber = 1)
        {
            ViewData["controllerName"] = "Products";
            ViewData["actioName"] = "Index";
            var productsList = _mapper.Map<IEnumerable<ProductViewModel>>(_productRepository.GetAll());
            return View(_pagination.GetProductsPage(productsList, pageNumber, Constants.ProductsOnPageCount));
        }

        public IActionResult ConfirmRemoving(Guid id)
        {
            return PartialView(_mapper.Map<ProductViewModel>(_productRepository.GetById(id)));
        }

        public IActionResult Remove(Guid id)
        {
            _productRepository.Remove(id);
            return RedirectToAction("Index");
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(viewModel);
                
                if (viewModel.UploadedFiles != null)
                {
                    var productImages = new List<ProductImage>();
                    foreach (var file in viewModel.UploadedFiles)
                    {
                        var imagePath = SaveFileWithNewUniqueImagePath(file, product.Brand + "_" + product.Title);
                        productImages.Add(new ProductImage { ImageUrl = imagePath });
                    }
                    product.ProductImages = productImages;
                }
                else
                {
                    product.ImageUrl = Path.Combine(_appEnvironment.WebRootPath, "\\pictures\\no_photo.jpg");
                }
                
                _productRepository.Add(product);
                return RedirectToAction("Index");
            }
            return View("Add", viewModel);
        }

        public IActionResult Edit(Guid id)
        {
            return View(_mapper.Map<ProductViewModel>(_productRepository.GetById(id)));
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(viewModel);
                if (viewModel.UploadedFiles != null)
                {
                    product.ProductImages = _productRepository.GetProductImages(product.Id).ToList();
                    foreach (var file in viewModel.UploadedFiles)
                    {
                        var imagePath = SaveFileWithNewUniqueImagePath(file, product.Brand + "_" + product.Title);
                        product.ProductImages.Add(new ProductImage { ImageUrl = imagePath });
                    }
                }
                _productRepository.Update(product);
                return RedirectToAction("Index");
            }
            return View("Edit", viewModel);
        }

        public IActionResult ShowProductDetails(Guid id)
        {
            return View (_mapper.Map<ProductViewModel>(_productRepository.GetById(id)));
        }

        private string SaveFileWithNewUniqueImagePath(IFormFile uploadedFile, string productName)
        {
            var productImagePath = Path.Combine(_appEnvironment.WebRootPath + _productsImagesPath);
            if (!Directory.Exists(productImagePath))
            {
                Directory.CreateDirectory(productImagePath);
            }
            var fileName = $"{productName}_{Guid.NewGuid()}.png";
            using (var stream = new FileStream(productImagePath + fileName, FileMode.Create))
            {
                var image = uploadedFile.ResizeImage(400, 400);
                image.Save(stream, ImageFormat.Png);
            }
            var imagePath = _productsImagesPath + fileName;

            return imagePath;
        }
    }
}
