using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace OnlineShopWebApp.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Введите марку!")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "Введите название!")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Введите цвет!")]
        public string Color { get; set; }

        [Required(ErrorMessage = "Введите год выпуска!")]
        [Range(1900, 2100, ErrorMessage = "Введите корректный год!")]
        public int ModelYear { get; set; }

        [Required(ErrorMessage = "Введите Страну-производитель!")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Введите цену!")]
        [Range(0, double.MaxValue, ErrorMessage ="Введите корректную цену!")]
        public decimal Cost { get; set; }

        public string ImageUrl { get; set; }

        public bool IsDeleted { get; set; }

        public IFormFile [] UploadedFiles { get; set; }

        public List<ProductImageViewModel> ProductImages { get; set; }

        public override string ToString()
        {
            return $"{Brand} {Title}";
        }
    }
}
