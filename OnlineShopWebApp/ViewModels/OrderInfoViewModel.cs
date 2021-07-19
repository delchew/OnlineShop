using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.ViewModels
{
    public class OrderInfoViewModel
    {
        public Guid Id { get; set; }

        public string Date { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentWay { get; set; }

        public string DeliveryType { get; set; }

        public string Note { get; set; }

        public string ReceiverName { get; set; }

        public string PhoneNumber { get; set; }

        public string Address { get; set; }

        public decimal TotalCost { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
