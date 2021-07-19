using Microsoft.EntityFrameworkCore;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineShop.DB.Services
{
    public class CartsDbRepository : ICartsRepository
    {
        private readonly DatabaseContext _databaseContext;

        public CartsDbRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public UserCart Get(string userId)
        {
            return _databaseContext.UserCarts.Include(x => x.CartItems)
                                                     .ThenInclude(x => x.Product)
                                                     .FirstOrDefault(x => x.UserId == userId);
        }

        public void RemoveCart(string userId)
        {
            _databaseContext.UserCarts.Remove(Get(userId));
            _databaseContext.SaveChanges();
        }

        public void Add(string userId, Product product, int quantity = 1)
        {
            var userCart = Get(userId);
            Func<CartItem> GetNewCartItem = () => new CartItem { Product = product, Quantity = quantity, UserCart = userCart };
            if (userCart == null)
            {
                userCart = new UserCart
                {
                    UserId = userId,
                    CartItems = new List<CartItem> { GetNewCartItem() }
                };
                _databaseContext.UserCarts.Add(userCart);
            }
            else
            {
                var cartItem = userCart.CartItems.FirstOrDefault(p => p.Product.Id == product.Id);
                if (cartItem == null)
                {
                    userCart.CartItems.Add(GetNewCartItem());
                }
                else
                {
                    cartItem.Quantity += quantity;
                }

            }

            _databaseContext.SaveChanges();
        }

        public void Remove(string userId, Product product)
        {
            var userCart = Get(userId);
            var cartItem = userCart.CartItems.FirstOrDefault(p => p.Product.Id == product.Id);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                }
                else
                {
                    userCart.CartItems.Remove(cartItem);
                }
            }
            _databaseContext.SaveChanges();
        }

        public void Clear(string userId)
        {
            var userCart = Get(userId);
            userCart.CartItems.ForEach(x => _databaseContext.Remove(x));
            _databaseContext.UserCarts.Remove(userCart);
            _databaseContext.SaveChanges();
        }
        
        public void Add(string userId, UserCart userCart)
        {
            var currentUserCart = Get(userId);
            if(userCart != null && userCart.CartItems != null)
            {
                foreach (var cartItem in userCart.CartItems)
                {
                    if (currentUserCart == null)
                    {
                        currentUserCart = new UserCart
                        {
                            UserId = userId,
                            CartItems = new List<CartItem> { cartItem }
                        };
                        cartItem.UserCart = currentUserCart;
                        _databaseContext.CartItems.Update(cartItem);
                        _databaseContext.UserCarts.Add(userCart);
                    }
                    else
                    {
                        var currentCartItem = currentUserCart.CartItems.FirstOrDefault(p => p.Product.Id == cartItem.Product.Id);
                        if (currentCartItem == null)
                        {
                            cartItem.UserCart = currentUserCart;
                            _databaseContext.CartItems.Update(cartItem);
                            currentUserCart.CartItems.Add(cartItem);
                        }
                        else
                        {
                            currentCartItem.Quantity += cartItem.Quantity;
                            _databaseContext.CartItems.Remove(cartItem);
                        }
                    }                
                }
                _databaseContext.UserCarts.Remove(userCart);
                _databaseContext.SaveChanges();
            }
        }
    }
}
