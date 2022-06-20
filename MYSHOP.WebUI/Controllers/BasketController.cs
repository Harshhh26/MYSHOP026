﻿using MYSHOP.CORE.Contracts;
using MYSHOP.CORE.Models;
using MYSHOP.DataAcess.SQL;
using MYSHOP.SERVICES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYSHOP.WebUI.Controllers
{
    public class BasketController : Controller
    {
        SQLRepository<Customer> Customers;
        IBasketService basketService;
        IorderService OrderService;

        public BasketController(BasketService basketService, IorderService OrderService, SQLRepository<Customer> Customers)
        {
            this.basketService = basketService;
            this.OrderService = OrderService;
            this.Customers = Customers;
        }
        // GET: Basket
        public ActionResult Index()
        {
            var model = basketService.GetBasketItem(this.HttpContext);
            return View(model);
        }
        public ActionResult AddToBasket(string Id)
        {
            basketService.AddToBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public ActionResult RemoveFromBasket(string Id)
        {
            basketService.RemoveFromBasket(this.HttpContext, Id);
            return RedirectToAction("Index");
        }
        public PartialViewResult BasketSummary()
        {
            var basketsummary = basketService.GetBasketSummary(this.HttpContext);
            return PartialView(basketsummary);

        }
        [Authorize]
        public ActionResult CheckOut()
        {
            Customer customer = Customers.collection().FirstOrDefault(c=>c.Email == User.Identity.Name);
            if (customer != null)
            {
                Order order = new Order()
                {

                    Email = customer.Email,
                    City = customer.City,
                    State = customer.State,
                    Street = customer.Street,
                    Firstname = customer.FirstName,
                    Surname = customer.LastName,
                    Zipcode = customer.ZipCode,
                };
                return View(order);
            }
            else {

                return RedirectToAction("Error");
            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult CheckOut(Order order)
        {
            var BasketItem = basketService.GetBasketItem(this.HttpContext);
            order.OrderStatus = "Order Created";
            order.Email = User.Identity.Name;

            //process payment

            order.OrderStatus = "Payment Processed";
            OrderService.CreateOrder(order, BasketItem);
            basketService.ClearBasket(this.HttpContext);

            return RedirectToAction("ThankYou", new { orderId=order.Id});

        }
        public ActionResult ThankYou(string orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }

    }
}