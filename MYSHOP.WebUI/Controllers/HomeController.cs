using MYSHOP.CORE.Models;
using MYSHOP.CORE.ViewModels;
using MYSHOP.DataAcess.SQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MYSHOP.WebUI.Controllers
{
    public class HomeController : Controller
    {
        SQLRepository<Product> context;
        SQLRepository<ProductCategory> productCategories;

        public HomeController(SQLRepository<Product> productcontext, SQLRepository<ProductCategory> productCategorycontext)
        {
            context = productcontext;
            productCategories = productCategorycontext;
        }

        public ActionResult Index(string Category = null)
        {
            List<Product> products;
            List<ProductCategory> categories = productCategories.collection().ToList();
            if (Category == null)
            {
               products =   context.collection().ToList();
            }
            else
            {
                products = context.collection().Where(p => p.Category == Category).ToList();

            }
            ProductListViewModel model = new ProductListViewModel();
            model.Products = products;
            model.productCategories = categories;
            return View(model);
        
        }
        public ActionResult Details(string Id)
        {
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
                
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}