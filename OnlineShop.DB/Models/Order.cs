using System;
using System.Collections.Generic;

namespace OnlineShop.DB.Models
{
    public class Order
    {
        public Guid Id { get; set; }

        public DateTime Date { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentWay PaymentWay { get; set; }

        public DeliveryType DeliveryType { get; set; }

        public string Note { get; set; }

        public string UserId { get; set; }


        public DeliveryContact DeliveryContact { get; set; }

        public Address Address { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}
