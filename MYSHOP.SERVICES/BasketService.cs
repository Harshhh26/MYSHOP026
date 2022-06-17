using MYSHOP.CORE.Contracts;
using MYSHOP.CORE.Models;
using MYSHOP.CORE.ViewModels;
using MYSHOP.DataAcess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace MYSHOP.SERVICES
{
    public class BasketService : IBasketService
    {
        SQLRepository<Product> productContext;
        SQLRepository<Basket> basketContext;

        public const string BasketSessionName = "eCommerceBasket";
        public BasketService(SQLRepository<Product> ProductContext, SQLRepository<Basket> BasketContext)
        {
            this.basketContext = BasketContext;
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

            HttpCookie cookie = new HttpCookie(BasketSessionName); 
            cookie.Value = basket.Id;
            cookie.Expires = DateTime.Now.AddDays(1);
            httpContext.Response.Cookies.Add(cookie);

            return basket;
        }
        public void AddToBasket(HttpContextBase httpContext, string productId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i => i.ProductId == productId);

            if (item==null)
            {
                item = new BasketItem()
                {
                    BasketId = basket.Id,
                    ProductId = productId,
                    Quantiy = 1

                };
                basket.BasketItems.Add(item);
            }
            else {

                item.Quantiy = item.Quantiy + 1;
            }
            basketContext.Commit();
        }
        public void RemoveFromBasket(HttpContextBase httpContext, string itemId)
        {
            Basket basket = GetBasket(httpContext, true);
            BasketItem item = basket.BasketItems.FirstOrDefault(i=> i.Id == itemId );

            if (item != null)
            {
                basket.BasketItems.Remove(item);
                basketContext.Commit();
            }
        }
        public List<BasketItemViewModel> GetBasketItem(HttpContextBase httpcontext)
        {
            Basket basket = GetBasket(httpcontext, false);

            if (basket != null)
            {
                var results = (from b in basket.BasketItems
                               join p in productContext.collection() on b.ProductId equals p.Id
                               select new BasketItemViewModel()
                               {
                                   Id = b.Id,
                                   Quanity = b.Quantiy,
                                   ProductName = p.Name,
                                   Image = p.Image,
                                   Price = p.Price
                               }
                              ).ToList();
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
                                    select item.Quantiy).Sum();

                decimal? basketTotal = (from item in basket.BasketItems
                                        join p in productContext.collection() on item.ProductId equals p.Id
                                        select item.Quantiy * p.Price).Sum();

                model.BasketCount = basketCount ?? 0;
                model.BasketTotal = basketTotal ?? decimal.Zero;

                return model;
            }
            else {
                return model;
                
            }
        }
    }
}
