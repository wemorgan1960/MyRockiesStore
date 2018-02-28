﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Core.Contracts;
using Store.Core.Models;
using System.Net.Mail;
using System.Net;

namespace Store.WebUI.Controllers
{

    public class BasketController : Controller
    {
        //private PayPal.Api.Payment payment;

        IRepository<Customer> customers;
        IBasketService basketService;
        IOrderService orderService;

        public BasketController(IBasketService BasketService, IOrderService OrderService, IRepository<Customer> Customers)
        {
            this.basketService = BasketService;
            this.orderService = OrderService;
            this.customers = Customers;
        }
        // GET: Basket2
        public ActionResult Index()
        {
            ViewBag.IsIndexHome = false;
            var model = basketService.GetBasketItems(this.HttpContext);
            return View(model);
        }
        [Authorize(Roles = RoleName.AskAdmin + "," + RoleName.AskUser)]
        [HttpPost]
        public ActionResult AddToBasket(string Id)
        {
            ViewBag.IsIndexHome = false;
            basketService.AddToBasket(this.HttpContext, Id);

            return RedirectToAction("Index","Products");
        }
        [Authorize(Roles = RoleName.AskAdmin + "," + RoleName.AskUser)]
        public ActionResult RemoveFromBasket(string Id)
        {
            ViewBag.IsIndexHome = false;
            basketService.RemoveFromBasket(this.HttpContext, Id);

            return RedirectToAction("Index","Products");
        }

        public PartialViewResult BasketSummary()
        {
            ViewBag.IsIndexHome = false;
            var basketSummary = basketService.GetBasketSummary(this.HttpContext);

            return PartialView(basketSummary);
        }

        [Authorize(Roles = RoleName.AskAdmin + "," + RoleName.AskUser)]
        public ActionResult Checkout()
        {
            ViewBag.IsIndexHome = false;
            Customer customer = customers.Collection().FirstOrDefault(c => c.Email == User.Identity.Name);

            if (customer != null)
            {
                var model = basketService.GetBasketItems(this.HttpContext);
                if (model != null)
                {
                    return View(customer);
                }
                else
                {
                    return RedirectToAction("Index", "Products");
                }

            }
            else
            {
                return RedirectToAction("Error");
            }

        }
        [Authorize(Roles = RoleName.AskAdmin + "," + RoleName.AskUser)]
        public ActionResult ReviewOrder()
        {
            ViewBag.IsIndexHome = false;
            var model = basketService.GetBasketItems(this.HttpContext);
            if (model != null)
            {
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Products");
            }

        }

        [HttpPost]
        //[Authorize(Roles = RoleName.AskAdmin + "," + RoleName.AskUser)]
        public void PlaceOrder()
        {
            var basketItems = basketService.GetBasketItems(this.HttpContext);
            var order = new Core.Models.Order
            {
                OrderNumber = Common.GetRandomInvoiceNumber()
            };
            order.InvoiceNumber = "Shop.MyRockies.Network" + @DateTime.Now.Year + order.OrderNumber;
            order.OrderStatus = "Order Created";
            orderService.CreateOrder(order, basketItems);
            order.OrderStatus = "Payment Processed";
            //order.CompletedAt = DateTime.Now; 
            orderService.UpdateOrder(order);

            //Email Customer
            string CustomerEmail = User.Identity.Name; ;

            var subject = "Store.com New Order: " + order.OrderNumber + " Recieved";
            var fromAddress = "admin@askyourmechanicdon.com";
            var toAddress = CustomerEmail;
            var emailBody = "Email From: Store.com Message: Thank you for your order: " + order.OrderNumber;

            var smtp = new SmtpClient();
            {
                smtp.Host = "smtp.myrockies.network";
                smtp.Port = 587;
                smtp.EnableSsl = false;
                smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp.Credentials = new NetworkCredential("admin@myrockies.network", "TtLUVAz5");
                smtp.Timeout = 20000;
            }

            smtp.Send(fromAddress, toAddress, subject, emailBody);


            //Email Admin 
            subject = "Store.com New Question: " + order.OrderNumber;
            fromAddress = "admin@askyourmechanicdon.com";
            toAddress = "admin@askyourmechanicdon.com";
            emailBody = "Email From: Store.com Message: A New Question: " + order.OrderNumber;

            var smtp1 = new SmtpClient();
            {
                smtp1.Host = "smtp.askyourmechanicdon.com";
                smtp1.Port = 587;
                smtp1.EnableSsl = false;
                smtp1.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
                smtp1.Credentials = new NetworkCredential("admin@askyourmechanicdon.com", "TtLUVAz5");
                smtp1.Timeout = 20000;
            }

            smtp1.Send(fromAddress, toAddress, subject, emailBody);

            basketService.ClearBasket(this.HttpContext);

        }

    }
}