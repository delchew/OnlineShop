using Microsoft.AspNetCore.Identity;

namespace OnlineShop.DB.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public bool IsBlocked { get; set; }

        public string AvatarImageUrl { get; set; }
    }
}
