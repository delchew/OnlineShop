using OnlineShop.DB.Models;

namespace OnlineShop.DB.Services.Interfaces
{
    public interface ICartsRepository
    {
        UserCart Get(string userId);

        void Add(string userId, Product product, int quantity = 1);

        void Add(string userId, UserCart userCart);

        void Remove(string userId, Product product);

        void RemoveCart(string userId);

        void Clear(string userId);
    }
}
