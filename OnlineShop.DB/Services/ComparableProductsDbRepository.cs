using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.DB.Services
{
    public class ComparableProductsDbRepository : IComparableProductsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ComparableProductsDbRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int GetProductsCount(string UserId)
        {
            return _databaseContext.UserComparableProducts.Where(u => u.UserId == UserId).Count();
        }

        public void Add(string UserId, Product product)
        {
            _databaseContext.UserComparableProducts.Add(new UserComparableProduct { ProductId = product.Id, UserId = UserId });
            _databaseContext.SaveChanges();
        }

        public void Clear(string UserId)
        {
            var userComparableProducts = _databaseContext.UserComparableProducts.Where(u => u.UserId == UserId).ToList();
            _databaseContext.UserComparableProducts.RemoveRange(userComparableProducts);
            _databaseContext.SaveChanges();
        }

        public bool Contains(string UserId, Guid id)
        {
            return GetByEmailAndProductId(UserId, id) != null;
        }

        public IEnumerable<Product> GetAll(string UserId)
        {
            return _databaseContext.UserComparableProducts.Where(u => u.UserId == UserId)
                                                          .Include(u => u.Product)
                                                          .Select(u => u.Product)
                                                          .ToList();
        }

        public void Remove(string userEmail, Guid id)
        {
            var removingFavorite = GetByEmailAndProductId(userEmail, id);
            _databaseContext.UserComparableProducts.Remove(removingFavorite);
            _databaseContext.SaveChanges();
        }

        private UserComparableProduct GetByEmailAndProductId(string UserId, Guid id)
        {
            return _databaseContext.UserComparableProducts.FirstOrDefault(u => u.UserId == UserId && u.ProductId == id);
        }

    }
}
