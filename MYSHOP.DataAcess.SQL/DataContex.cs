﻿using MYSHOP.CORE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MYSHOP.DataAcess.SQL;



namespace MYSHOP.DataAcess.SQL
{
    public class DataContext : DbContext
    {
        public DataContext()
            : base("DefaultConnection") {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<Customer> Customers { get; set; } 
 
    }

}
