﻿using MYSHOP.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MYSHOP.CORE.Contracts
{
    public interface IBasketService
    {
        void AddToBasket(HttpContextBase httpContext, string productId);
        void RemoveFromBasket(HttpContextBase httpContext, string itemId);
        List<BasketItemViewModel> GetBasketItem(HttpContextBase httpcontext);
        BasketSummaryViewModel GetBasketSummary(HttpContextBase httpContext);
    }
}
