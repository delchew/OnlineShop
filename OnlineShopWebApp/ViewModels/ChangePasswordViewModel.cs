using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewModels
{
    public class ChangePasswordViewModel
    {
        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Введите пароль!")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Введите подтверждение для нового пароля!")]
        [Compare(nameof(NewPassword), ErrorMessage = "Пароли не совпадают!")]
        public string ConfirmNewPassword { get; set; }
    }
}
