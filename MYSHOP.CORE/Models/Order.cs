﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYSHOP.CORE.Models
{
   public  class Order : BaseEntity
    {
        public Order()
        {
            this.OrderItem = new List<OrderItem>();     
        }
        public string Firstname { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string OrderStatus { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }


    }
}
