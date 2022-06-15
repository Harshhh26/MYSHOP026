using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYSHOP.CORE.Models;
//using MYSHOP.DataAccess.InMemory;
using MYSHOP.CORE.ViewModels;
using MYSHOP.CORE.Contracts;
using MYSHOP.DataAcess.SQL;

namespace MYSHOP.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        SQLRepository<Product> context;
        SQLRepository<ProductCategory> productCategories;

        public ProductManagerController(SQLRepository<Product> productcontext, SQLRepository<ProductCategory> productCategorycontext)
        {
            context = productcontext;
            productCategories = productCategorycontext;
        }

        // GET: ProductManager
        public ActionResult Index()
        {
            List<Product> products = context.collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ProductManagerViewModel ViewModel = new ProductManagerViewModel();

            ViewModel.Product = new Product();
            ViewModel.productCategories = productCategories.collection();
            return View(ViewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (!ModelState.IsValid) 
            {
                return View(product);
            }
            else
            {
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string ID)
        {
            Product product = context.Find(ID);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel();
                viewModel.Product = product;
                viewModel.productCategories = productCategories.collection();   
                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, String ID)
        {
            Product productToEdit = context.Find(ID);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    return View(product);
                }
                productToEdit.Category = product.Category;
                productToEdit.Depriciation = product.Depriciation;
                productToEdit.Image = product.Image;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index"); 
            }
        }

        public ActionResult Delete(string ID)
        {
            Product productToDelete = context.Find(ID);
            if (productToDelete  == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(productToDelete);
            }
        }
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult ConfirmDelete(string ID)
        {
            Product productToDelete = context.Find(ID);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(ID);
                context.Commit();
                return RedirectToAction("index");            }
        }
    }
}