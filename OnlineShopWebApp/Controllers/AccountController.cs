using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;

namespace OnlineShopWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ICartsRepository _cartsRepository;
        private readonly IFavoriteProductsRepository _favoriteProductsRepository;
        private readonly IComparableProductsRepository _comparableProductsRepository;

        public AccountController(SignInManager<User> signInManager, UserManager<User> userManager, ICartsRepository cartsRepository,
            IFavoriteProductsRepository favoriteProductsRepository, IComparableProductsRepository comparableProductsRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _cartsRepository = cartsRepository;
            _favoriteProductsRepository = favoriteProductsRepository;
            _comparableProductsRepository = comparableProductsRepository;
        }
        public IActionResult Login(string returnUrl)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel login)
        {
            if (login.Email == login.Password)
            {
                ModelState.AddModelError(string.Empty, "Логин и пароль не должны совпадать!");
            }

            var user = _userManager.FindByNameAsync(login.Email).Result;
            if (user != null)
            {
                var result = _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, false).Result;

                if (!result.Succeeded)
                {
                    ModelState.AddModelError(string.Empty, "Вы ввели неверный пароль!");
                }

                if (user.IsBlocked)
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким именем заблокирован!");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Пользователя с таким e-mail не существует, введите существующий e-mail или зарегистрируйтесь!");
            }

            if (ModelState.IsValid)
            {
                AddUnsignedCartItems(user.UserName);
                AddUnsignedProductsGroup(user.UserName, _favoriteProductsRepository);
                AddUnsignedProductsGroup(user.UserName, _comparableProductsRepository);

                if (login.ReturnUrl == null)
                {
                    return RedirectToAction("Index", "Home");
                }
                return Redirect(login.ReturnUrl);
            }

            return View(login);
        }

        public IActionResult SignOut()
        {
            _signInManager.SignOutAsync().Wait();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register(string returnUrl)
        {
            return View(new RegisterViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel register)
        {
            if (register.Email == register.Password)
            {
                ModelState.AddModelError(string.Empty, "Логин и пароль не должны совпадать");
            }

            if (_userManager.FindByNameAsync(register.Email).Result != null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь с таким e-mail уже существует, введите другой e-mail!");
            }

            if (ModelState.IsValid)
            {
                var newUser = new User { Email = register.Email, UserName = register.Email };
                var createResult = _userManager.CreateAsync(newUser, register.Password).Result;
                var assignRoleResult = _userManager.AddToRoleAsync(newUser, Constants.UserRoleName).Result;
                if (createResult.Succeeded)
                {
                    _signInManager.SignInAsync(newUser, true).Wait();

                    AddUnsignedCartItems(newUser.UserName);
                    AddUnsignedProductsGroup(newUser.UserName, _favoriteProductsRepository);
                    AddUnsignedProductsGroup(newUser.UserName, _comparableProductsRepository);

                    if (register.ReturnUrl == null)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    return Redirect(register.ReturnUrl);
                }
                else
                {
                    foreach(var error in createResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            return View(nameof(Register), register);
        }

        private void AddUnsignedCartItems(string userId)
        {
            if (HttpContext.Request.Cookies.TryGetValue(Constants.UnsignedUserId, out var unsignedUserId))
            {
                var unsignedUserCart = _cartsRepository.Get(unsignedUserId);
                _cartsRepository.Add(userId, unsignedUserCart);
            }
        }

        private void AddUnsignedProductsGroup(string userId, IProductsGroupRepository productsGroupRepository)
        {
            if (HttpContext.Request.Cookies.TryGetValue(Constants.UnsignedUserId, out var unsignedUserId))
            {
                var unsignedFavorites = productsGroupRepository.GetAll(unsignedUserId);
                foreach (var product in unsignedFavorites)
                {
                    if (!productsGroupRepository.Contains(userId, product.Id))
                    {
                        productsGroupRepository.Add(userId, product);
                    }
                }
                productsGroupRepository.Clear(unsignedUserId);
            }
        }
    }
}
