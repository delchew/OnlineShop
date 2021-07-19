using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShopWebApp.ViewModels
{
    public class OrderViewModel
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentWay PaymentWay { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string Note { get; set; }


        public DeliveryContactViewModel DeliveryContact { get; set; }

        public Address Address { get; set; }

        public List<CartItemViewModel> CartItems { get; set; }

        public decimal TotalCost => CartItems?.Sum(p => p.Product.Cost * p.Quantity) ?? 0;
    }
}
