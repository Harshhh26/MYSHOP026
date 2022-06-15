﻿using MYSHOP.CORE.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MYSHOP.DataAcess.SQL
{
    public class DataContex : DbContext
    {
        public DataContex()
            : base("DefaultConnection") {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }


    }

}