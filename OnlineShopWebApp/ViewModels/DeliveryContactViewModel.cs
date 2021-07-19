using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewModels
{
    public class DeliveryContactViewModel
    {
        [Required(ErrorMessage = "Введите имя!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Ограничение по длине строки для имени: не менее 1 символа и не более 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Введите фамилию!")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Ограничение по длине строки для фамилии: не менее 1 символа и не более 50")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Номер телефона должен быть не короче 6 символов и не длинее 20")]
        public string PhoneNumber { get; set; }
    }
}
