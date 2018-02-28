﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Store.Core.Contracts;
using Store.Core.Models;
using Store.Core.ViewModels;

namespace Store.Services
{
    public class BasketService : IBasketService
    {
        IRepository<Product> productContext;
        IRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";

        public BasketService(IRepository<Product> ProductContext, IRepository<Basket> BasketContext)
        {
            this.basketContext=BasketContext;
            this.productContext = ProductContext;
        }

        private Basket GetBasket(HttpContextBase httpContext, bool createIfNull)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(BasketSessionName);

            Basket basket = new Basket();

            if (cookie != null)
            {
                string basketId = cookie.Value;
                if (!string.IsNullOrEmpty(basketId))
                {
                    basket = basketContext.Find(basketId);
                }
                else
                {
                    if (createIfNull)
                    {
                        basket = CreateNewBasket(httpContext);
                    }
                }
            }
            else
            {
                if (createIfNull)
                {
                    basket = CreateNewBasket(httpContext);
                }
            }
            return basket;
        }
        private Basket CreateNewBasket(HttpContextBase httpContext)
        {
            Basket basket = new Basket();
            basketContext.Insert(basket);
            basketContext.Commit();

            HttpCookie cookie = new HttpCookie(BasketSessionName)
            {
                Value = basket.Id,
                Expires = DateTime.Now.AddDays(1)
            };
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }
        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            if(productId != null)
            {
                Basket basket = GetBasket(httpContext, true);
                //BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

                //if (item == null)
                //{
                BasketItem item = new BasketItem()
                    {
                        BasketId = basket.Id,
                        ProductId = productId,
                        Quanity = 1
                    };
                    basket.BasketItems.Add(item);
                //}
                //else
                //{
                //    item.Quanity = item.Quanity + 1;
                //}
                basketContext.Commit();

            }

        }
        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.Id == itemId);

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
        public List<BasketItemViewModel>GetBasketItems(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);

            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                              join p in productContext.Collection()
                              on b.ProductId equals p.Id
                              select new BasketItemViewModel()
                              {
                                  Id = b.Id,
                                  Quanity = b.Quanity,
                                  ProductName = p.Name,
                                  Image = p.Image,
                                  Image2=p.Image2,
                                  Image3=p.Image3,
                                  Price = p.Price,
                                  Shipping=p.Shipping
                              }).ToList();
                return results;
            }
            else
            {
                return new List<BasketItemViewModel>();
            }
        }
        public BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            BasketSummaryViewModel model = new BasketSummaryViewModel(0, 0);

            if (basket != null)
            {
                int? basketCount = (from item in basket.BasketItems
                                    select item.Quanity).Sum();

                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.Collection() on item.ProductId equals p.Id
                                        select item.Quanity * p.Price).Sum();
                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }
            else
            {
                return model;
            }
        }
        public void ClearBasket(HttpContextBase httpContext)
        {
            Basket basket = GetBasket(httpContext, false);
            basket.BasketItems.Clear();
            basketContext.Commit();
        }
    }
}
