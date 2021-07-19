using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using OnlineShopWebApp.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]

    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        public UsersController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var models = _mapper.Map<IEnumerable<User>, IEnumerable<UserViewModel>>(_userManager.Users.ToList());
            return View(models);
        }

        public IActionResult Remove(string email)
        {
            var user =_userManager.FindByNameAsync(email).Result;
            user.IsBlocked = true;
            _userManager.UpdateAsync(user).Wait();
            return RedirectToAction("Index");
        }

        public IActionResult Edit(string email)
        {
            return View(_mapper.Map<UserViewModel>(_userManager.FindByNameAsync(email).Result));
        }

        [HttpPost]
        public IActionResult Edit(UserViewModel updaterUser)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(updaterUser.Email).Result;
                user.Name = updaterUser.Name;
                user.Surname = updaterUser.Surname;
                user.PhoneNumber = updaterUser.PhoneNumber;
                _userManager.UpdateAsync(user).Wait();
                return RedirectToAction("Index");
            }
            return View("Edit", updaterUser);
        }

        public IActionResult AssignRole(string userId)
        {
            var user = _userManager.FindByNameAsync(userId).Result;
            var assignRoleViewModel = new AssignRoleViewModel
            {
                RolesList = _roleManager.Roles.ToList(),
                UserId = userId,
                UserRoles = _userManager.GetRolesAsync(user).Result.ToList()
            };
            return View(assignRoleViewModel);
        }

        [HttpPost]
        public IActionResult AssignRole(string UserId, List<string> roles)
        {
            var user = _userManager.FindByNameAsync(UserId).Result;
            var userRoles = _userManager.GetRolesAsync(user).Result;
            var addedRoles = roles.Except(userRoles);
            var removedRoles = userRoles.Except(roles);
            _userManager.AddToRolesAsync(user, addedRoles).Wait();
            _userManager.RemoveFromRolesAsync(user, removedRoles).Wait();
            return RedirectToAction("Index");
        }
    }
}
