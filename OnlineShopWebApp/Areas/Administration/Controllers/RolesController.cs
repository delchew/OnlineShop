using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShopWebApp.ViewModels;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]

    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RolesController(RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<RoleViewModel>>(_roleManager.Roles.ToList()));
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(string roleName)
        {
            var newRole = new IdentityRole();
            if (_roleManager.FindByNameAsync(roleName).Result == null)
            {
                newRole.Name = roleName;
            }
            else
            {
                ModelState.AddModelError(string.Empty, errorMessage: "Роль с таким именем уже существует!");
            }

            if (ModelState.IsValid)
            {
                _roleManager.CreateAsync(newRole).Wait();
                return RedirectToAction("Index");
            }
            return View("Add", newRole);
        }

        public IActionResult Remove(string name)
        {
            var role = _roleManager.FindByNameAsync(name).Result;
            _roleManager.DeleteAsync(role).Wait();
            return RedirectToAction("Index");
        }
    }
}
