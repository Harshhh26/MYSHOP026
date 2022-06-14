﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MYSHOP.CORE;
using MYSHOP.CORE.Models;

namespace MYSHOP.DataAccess.InMemory
{
   public class ProductRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<Product> products = new List<Product>();

        public ProductRepository()
        {
            products = cache["products"] as List<Product>;
            if (products == null)
            {
                products = new List<Product>(); 
            }           
        }
        public void Commit()
        {
            cache["products"] = products;
        }
        public void Insert(Product p)
        {
            products.Add(p);
        }
        public void Update(Product product)
        {
            Product productToUpdate = products.Find(p => p.ID == product.ID);

            if (productToUpdate != null)
            {
                productToUpdate = product;
            }
            else
            {
                throw new Exception("product no found"); 
            }
        }
        public Product Find(string ID)
        {
            Product product = products.Find(p => p.ID == ID);

            if (product != null)
            {
                return  product;
            }
            else
            {
                throw new Exception("product category no found");
            }
        }

        public IQueryable<Product> collection()
        {
            return products.AsQueryable();
        }
        public void Delete(string ID)
        {
            Product productToDelete = products.Find(p => p.ID == ID);

            if (productToDelete != null)
            {
                products.Remove(productToDelete) ;
            }
            else
            {
                throw new Exception("product no found");
            }
        }
    }
}
