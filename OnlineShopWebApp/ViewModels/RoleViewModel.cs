using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage = "Введите название роли!")]
        public string Name { get; set; }
    }
}
