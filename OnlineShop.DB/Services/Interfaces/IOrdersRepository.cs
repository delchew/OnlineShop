using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.DB.Services.Interfaces
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> GetAll();

        Order GetById(Guid id);

        void Add(Order order);

        void Update(Order updaterOrder);
    }
}
