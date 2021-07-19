using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.DB.Services
{
    public class OrdersDbRepository : IOrdersRepository
    {
        private readonly DatabaseContext _databaseContext;

        public OrdersDbRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public IEnumerable<Order> GetAll()
        {
            return _databaseContext.Orders.Include(o => o.DeliveryContact)
                                          .Include(o => o.CartItems)
                                          .ThenInclude(c => c.Product);
        }

        public void Add(Order order)
        {
            _databaseContext.Orders.Add(order);
            _databaseContext.SaveChanges();
        }

        public Order GetById(Guid id)
        {
            return _databaseContext.Orders.Include(o => o.DeliveryContact)
                                          .Include(o => o.Address)
                                          .Include(o => o.CartItems).ThenInclude(c => c.Product)
                                          .FirstOrDefault(o => o.Id == id);
        }

        public void Update(Order order)
        {
            _databaseContext.Orders.Update(order);
            _databaseContext.SaveChanges();
        }
    }
}
