using OnlineShop.DB.Models;
using System;
using System.Collections.Generic;

namespace OnlineShop.DB.Services.Interfaces
{
    public interface IProductsRepository
    {
        IEnumerable<Product> GetAll();

        IEnumerable<Product> SearchByBrandOrTitleMatch(string searchString);

        Product GetById(Guid id);

        void Add(Product item);

        void Update(Product updaterProduct);

        void Remove(Guid id);

        IEnumerable<ProductImage> GetProductImages(Guid productId);
    }
}
