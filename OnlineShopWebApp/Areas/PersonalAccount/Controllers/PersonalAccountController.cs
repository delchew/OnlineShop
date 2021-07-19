using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.Extensions;
using OnlineShopWebApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace OnlineShopWebApp.Areas.PersonalAccount.Controllers
{
    [Area("PersonalAccount")]
    [Authorize]
    public class PersonalAccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICartsRepository _cartsRepository;
        private readonly IFavoriteProductsRepository _favoriteProductsRepository;
        private readonly IComparableProductsRepository _comparableProductsRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;
        private readonly string _userAvatarsPath = "\\pictures\\userAvatars\\";

        public PersonalAccountController(SignInManager<User> signInManager, UserManager<User> userManager, ICartsRepository cartsRepository,
            IFavoriteProductsRepository favoriteProductsRepository, IComparableProductsRepository comparableProductsRepository, IWebHostEnvironment appEnvironment,
            IOrdersRepository ordersRepository, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cartsRepository = cartsRepository;
            _favoriteProductsRepository = favoriteProductsRepository;
            _comparableProductsRepository = comparableProductsRepository;
            _appEnvironment = appEnvironment;
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_mapper.Map<UserViewModel>(_userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result));
        }

        public IActionResult Edit()
        {
            return View(_mapper.Map<UserViewModel>(_userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result));
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel userViewModel)
        {
            if(ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(userViewModel.UserName).Result;
                user.Name = userViewModel.Name;
                user.Surname = userViewModel.Surname;
                user.PhoneNumber = userViewModel.PhoneNumber;

                if (userViewModel.UploadedFile != null)
                {
                    var avatarImagePath = SaveFileWithNewUniqueImagePath(userViewModel.UploadedFile, userViewModel.Email);
                    user.AvatarImageUrl = avatarImagePath;
                }
                var result =_userManager.UpdateAsync(user).Result;
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                result.Errors.ToList().ForEach(err => ModelState.AddModelError(string.Empty, err.Description));
            }
            return View(userViewModel);
        }

        public IActionResult DeleteAvatar()
        {
            var user = _userManager.FindByNameAsync(HttpContext.User.Identity.Name).Result;
            user.AvatarImageUrl = null;
            _userManager.UpdateAsync(user).Wait();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Orders()
        {
            return View(_mapper.Map<IEnumerable<OrderViewModel>>(_ordersRepository.GetAll()?.Where(x => x.UserId == HttpContext.User.Identity.Name)));
        }

        public IActionResult SpecifiedOrder(Guid orderId)
        {
            return View(_mapper.Map<OrderInfoViewModel>(_ordersRepository.GetById(orderId)));
        }

        public IActionResult ChangePassword()
        {
            return View(new ChangePasswordViewModel { UserName = HttpContext.User.Identity.Name });
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel changePassword)
        {
            if (changePassword.UserName == changePassword.Password)
            {
                ModelState.AddModelError(string.Empty, "Логин и пароль не должны совпадать");
            }
            if (changePassword.Password == changePassword.NewPassword)
            {
                ModelState.AddModelError(string.Empty, "Старый и новый пароли не должны совпадать");
            }

            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(changePassword.UserName).Result;
                _userManager.ChangePasswordAsync(user, changePassword.Password, changePassword.NewPassword).Wait();
                return RedirectToAction(nameof(Index));
            }
            return View("ChangePassword", changePassword);
        }

        private string SaveFileWithNewUniqueImagePath(IFormFile uploadedFile, string userEmail)
        {
            var avatarImagePath = Path.Combine(_appEnvironment.WebRootPath + _userAvatarsPath);
            if (!Directory.Exists(avatarImagePath))
            {
                Directory.CreateDirectory(avatarImagePath);
            }
            var fileName = $"{userEmail}_avatar.png";
            using (var stream = new FileStream(avatarImagePath + fileName, FileMode.Create))
            {
                var image = uploadedFile.ResizeImage(200, 200);
                image.Save(stream, ImageFormat.Png);
            }
            var imagePath = _userAvatarsPath + fileName;

            return imagePath;
        }
    }
}
