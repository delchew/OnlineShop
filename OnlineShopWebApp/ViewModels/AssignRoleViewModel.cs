using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace OnlineShopWebApp.ViewModels
{
    public class AssignRoleViewModel
    {
        public string UserId { get; set; }

        public List<IdentityRole> RolesList { get; set; }

        public List<string> UserRoles { get; set; }

        public AssignRoleViewModel()
        {
            RolesList = new List<IdentityRole>();
            UserRoles = new List<string>();
        }
    }
}
