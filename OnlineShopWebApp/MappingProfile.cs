using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnlineShop.DB.Models;
using OnlineShopWebApp.Extensions;
using OnlineShopWebApp.ViewModels;
using System;
using System.Linq;

namespace OnlineShopWebApp
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CartItem, CartItemViewModel>().ReverseMap();
            CreateMap<DeliveryContact, DeliveryContactViewModel>().ReverseMap();
            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<IdentityRole, RoleViewModel>().ReverseMap();
            CreateMap<UserCart, UserCartViewModel>().ReverseMap();
            CreateMap<User, UserViewModel>().ReverseMap();
            CreateMap<ProductImage, ProductImageViewModel>().ReverseMap();

            CreateMap<Order, OrderInfoViewModel>()
                .ForMember(o => o.Date, opt => opt.MapFrom(o => o.Date.ToShortDateString()))
                .ForMember(o => o.OrderStatus, opt => opt.MapFrom(o => o.OrderStatus.GetDescription()))
                .ForMember(o => o.PaymentWay, opt => opt.MapFrom(o => o.PaymentWay.GetDescription()))
                .ForMember(o => o.DeliveryType, opt => opt.MapFrom(o => o.DeliveryType.GetDescription()))
                .ForMember(o => o.ReceiverName, opt => opt.MapFrom(o => o.DeliveryContact.Name + " " + o.DeliveryContact.Surname))
                .ForMember(o => o.PhoneNumber, opt => opt.MapFrom(o => o.DeliveryContact.PhoneNumber))
                .ForMember(o => o.Address, opt => opt.MapFrom(o => o.Address.ToString()))
                .ForMember(o => o.TotalCost, opt => opt.MapFrom(o => o.CartItems.Sum(c => c.Product.Cost * c.Quantity)));

            CreateMap<OrderViewModel, Order>()
                .ForMember(o => o.OrderStatus, opt => opt.MapFrom(o => OrderStatus.AwaitingConfirm))
                .ForMember(o => o.Date, opt => opt.MapFrom(o => DateTime.Now));
            CreateMap<Order, OrderViewModel>();
        }
    }
}
