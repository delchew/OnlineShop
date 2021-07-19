using System.Linq;
using System.Collections.Generic;
using System;
using OnlineShop.DB.Services.Interfaces;
using OnlineShop.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace OnlineShop.DB.Services
{
    public class FavoriteProductsDbRepository : IFavoriteProductsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public FavoriteProductsDbRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public int GetProductsCount(string UserId)
        {
            return _databaseContext.UserFavoriteProducts.Where(u => u.UserId == UserId).Count();
        }

        public int GetAllFavoritesCount()
        {
            return _databaseContext.UserFavoriteProducts.Count();
        }

        public void Add(string UserId, Product product)
        {
            _databaseContext.UserFavoriteProducts.Add(new UserFavoriteProduct { ProductId = product.Id, UserId = UserId });
            _databaseContext.SaveChanges();
        }

        public void Clear(string UserId)
        {
            var userFavoriteProducts = _databaseContext.UserFavoriteProducts.Where(u => u.UserId == UserId).ToList();
            _databaseContext.UserFavoriteProducts.RemoveRange(userFavoriteProducts);
            _databaseContext.SaveChanges();
        }

        public bool Contains(string UserId, Guid id)
        {
            return GetByEmailAndProductId(UserId, id) != null;
        }

        public IEnumerable<Product> GetAll(string UserId)
        {
            return _databaseContext.UserFavoriteProducts.Where(u => u.UserId == UserId)
                                                        .Include(u => u.Product)
                                                        .ThenInclude(p => p.ProductImages)
                                                        .Select(u => u.Product)
                                                        .ToList();
        }

        public void Remove(string UserId, Guid id)
        {
            var removingFavorite = GetByEmailAndProductId(UserId, id);
            _databaseContext.UserFavoriteProducts.Remove(removingFavorite);
            _databaseContext.SaveChanges();
        }

        private UserFavoriteProduct GetByEmailAndProductId(string UserId, Guid id)
        {
            return _databaseContext.UserFavoriteProducts.FirstOrDefault(u => u.UserId == UserId && u.ProductId == id);
        }
    }
}
