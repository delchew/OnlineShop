using System;
using System.Collections.Generic;
using OnlineShop.DB.Models;

namespace OnlineShop.DB.Services.Interfaces
{
    public interface IProductsGroupRepository
    {
        int GetProductsCount(string userId);

        IEnumerable<Product> GetAll(string userId);

        void Add(string userId, Product product);

        void Remove(string userId, Guid id);

        void Clear(string userId);

        bool Contains(string userId, Guid Id);
    }
}
