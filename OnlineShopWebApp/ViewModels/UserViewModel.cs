using Microsoft.AspNetCore.Http;
using OnlineShop.DB;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Введите e-mail!")]
        [RegularExpression(Constants.EmailRegex, ErrorMessage = "Введите валидный e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Введите имя!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Ограничение по длине строки для имени: не менее 1 символа и не более 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Ограничение по длине строки для фамилии: не менее 1 символа и не более 50")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Номер телефона должен быть не короче 6 символов и не длинее 20")]
        public string PhoneNumber { get; set; }

        public bool IsBlocked { get; set; }

        public IFormFile UploadedFile { get; set; }

        public string AvatarImageUrl { get; set; }
    }
}
