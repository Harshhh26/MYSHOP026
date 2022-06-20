using MYSHOP.CORE.Models;
using MYSHOP.CORE.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYSHOP.CORE.Contracts
{
   public interface IorderService
    {
        void CreateOrder(Order BaseOrder, List<BasketItemViewModel> basketItems);
        List<Order> GetOrdersList();
        Order GetOrder(String Id);
        void UpdateOrder(Order updatedorder);

    }
}
