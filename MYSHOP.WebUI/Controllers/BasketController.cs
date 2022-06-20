using MYSHOP.CORE.Contracts;
using MYSHOP.CORE.Models;
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
        IBasketService basketService;
        IorderService OrderService;

        public BasketController(BasketService basketService, IorderService OrderService)
        {
            this.basketService = basketService;
            this.OrderService = OrderService;
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
        public ActionResult CheckOut()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CheckOut(Order order)
        {
            var BasketItem = basketService.GetBasketItem(this.HttpContext);
            order.OrderStatus = "Order Created";

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