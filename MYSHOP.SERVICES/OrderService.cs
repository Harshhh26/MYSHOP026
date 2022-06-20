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

                BaseOrder.OrderItem.Add(new OrderItem() {
                    ProductId = item.Id,
                    Image = item.Image,
                    Price = item.Price,
                    ProductName = item.ProductName,
                    Quanity = item.Quanity,
            });
            Ordercontext.Insert(BaseOrder);
            Ordercontext.Commit();
        }
        public List<Order> GetOrdersList()
        {
            return Ordercontext.collection().ToList();
        }
        public Order GetOrder(string Id)
        {
            return Ordercontext.Find(Id);
        }
        public void UpdateOrder(Order updatedorder)
        {
            Ordercontext.Update(updatedorder);
            Ordercontext.Commit();
        }
    }
}
