using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.DB;
using OnlineShop.DB.Models;
using OnlineShop.DB.Services.Interfaces;
using OnlineShopWebApp.ViewModels;
using System;
using System.Collections.Generic;

namespace OnlineShopWebApp.Areas.Administration.Controllers
{
    [Area("Administration")]
    [Authorize(Roles = Constants.AdminRoleName)]
    public class OrdersController : Controller
    {
        private readonly IOrdersRepository _ordersRepository;
        private readonly IMapper _mapper;

        public OrdersController(IOrdersRepository ordersRepository, IMapper mapper)
        {
            _ordersRepository = ordersRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View(_mapper.Map<IEnumerable<OrderViewModel>>(_ordersRepository.GetAll()));
        }

        public IActionResult Edit(Guid id)
        {
            return View(_mapper.Map<OrderViewModel>(_ordersRepository.GetById(id)));
        }

        [HttpPost]
        public IActionResult Edit(Guid id, OrderStatus orderStatus)
        {
            var order =_ordersRepository.GetById(id);
            order.OrderStatus = orderStatus;
            _ordersRepository.Update(order);
            return RedirectToAction("Index");
        }
    }
}
