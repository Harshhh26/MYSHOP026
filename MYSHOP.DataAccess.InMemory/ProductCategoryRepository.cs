 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Caching;
using MYSHOP.CORE.Models;
using MYSHOP.DataAccess.InMemory;

namespace MYSHOP.DataAccess.InMemory
{
    public  class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productcatogries;        

        public ProductCategoryRepository()
        {
            productcatogries = cache["productcategories"] as List<ProductCategory>;
            if (productcatogries == null)
            {
                productcatogries = new List<ProductCategory>(); 
            }
        }
        public void Commit()
        {
            cache["productcategories"] = productcatogries;
        }
        public void Insert(ProductCategory p)
        {
            productcatogries.Add(p);
        }
        public void Update(ProductCategory productcategory)
        {
            ProductCategory productCategoryToUpdate = productcatogries.Find(p => p.Id == productcategory.Id);

            if (productCategoryToUpdate != null)
            {
                productCategoryToUpdate = productcategory;
            }
            else
            {
                throw new Exception("product category no found");
            }
        }
        public ProductCategory Find(string Id)
        {
            ProductCategory productCategory = productcatogries.Find(p => p.Id == Id);

            if (productCategory != null)
            {
                return productCategory;
            }
            else
            {
                throw new Exception("productcategory no found");
            }
        }

        public IQueryable<ProductCategory> collection()
        {
            return productcatogries.AsQueryable();
        }
        public void Delete(string Id)
        {
            ProductCategory productCategoryToDelete = productcatogries.Find(p => p.Id == Id);

            if (productCategoryToDelete != null)
            {
                productcatogries.Remove(productCategoryToDelete);
            }
            else
            {
                throw new Exception("productcategory  no found");
            }
        }
    }

}

