using MYSHOP.CORE.Contracts;
using MYSHOP.CORE.Models;
using MYSHOP.CORE.ViewModels;
using MYSHOP.DataAcess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYSHOP.SERVICES
{
    public class OrderService : IorderService
    {
        SQLRepository<Order> Ordercontext;
        public OrderService(SQLRepository<Order> Ordercontext)
        {
            this.Ordercontext = Ordercontext;
        }

        public void CreateOrder(Order BaseOrder, List<BasketItemViewModel> basketItems)
        {
            foreach (var item in basketItems)

                BaseOrder.OrderItems.Add(new OrderItem() {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quanity = item.Quanity,
            });
            Ordercontext.Insert(BaseOrder);
            Ordercontext.Commit();
        }
    }
}
