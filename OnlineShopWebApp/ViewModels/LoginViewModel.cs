using OnlineShop.DB;
using System.ComponentModel.DataAnnotations;


namespace OnlineShopWebApp.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введите e-mail!")]
        [RegularExpression(Constants.EmailRegex, ErrorMessage = "Введите валидный e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Пароль должен быть не короче 4 и не длинее 50 символов")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
