using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;

namespace OnlineShop.DB.Services
{
    public class ProductsDbRepository : IProductsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public ProductsDbRepository(DatabaseContext databaseContext)
        {
            this._databaseContext = databaseContext;
        }

        public IEnumerable<Product> GetAll()
        {
            return _databaseContext.Products.Include(x => x.ProductImages)
                                            .AsNoTracking();
        }

        public Product GetById(Guid id)
        {
            return _databaseContext.Products.Include(x => x.ProductImages)
                                            .FirstOrDefault(x => x.Id == id);
        }

        public void Add(Product product)
        {
            _databaseContext.Products.Add(product);
            _databaseContext.SaveChanges();
        }

        public void Remove(Guid id)
        {
            var product = _databaseContext.Products.Include(x => x.CartItems)
                                                   .Include(x => x.ProductImages)
                                                   .FirstOrDefault(x => x.Id == id);
            if(product.CartItems.Count == 0)
            {
                if(product.ProductImages != null)
                {
                    foreach (var image in product.ProductImages)
                    {
                        if (File.Exists(image.ImageUrl))
                        {
                            File.Delete(image.ImageUrl);
                        }
                    }
                }
                _databaseContext.Products.Remove(product);
            }
            else
            {
                product.IsDeleted = true;
                _databaseContext.Products.Update(product);
            }

            _databaseContext.SaveChanges();
        }

        public void Update(Product product)
        {
            _databaseContext.Products.Update(product);
            _databaseContext.SaveChanges();
        }

        public IEnumerable<Product> SearchByBrandOrTitleMatch(string searchString) //Пока сделал простой поиск
        {
            return _databaseContext.Products.Where(p => (p.Brand + p.Title).Contains(searchString))
                                            .Include(x => x.ProductImages)
                                            .AsNoTracking();
        }

        public IEnumerable<ProductImage> GetProductImages(Guid productId)
        {
            return _databaseContext.ProductImages.Where(x => x.ProductId == productId).ToList();
        }
    }
}
