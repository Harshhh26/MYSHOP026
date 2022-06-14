using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYSHOP.CORE.Models;
using MYSHOP.DataAccess.InMemory;
using MYSHOP.CORE.ViewModels;


namespace MYSHOP.WebUI.Controllers
{
    public class ProductManagerController : Controller
    {
        InMemoryRepository<Product> context;
        InMemoryRepository<ProductCategory> productCategories;

        public ProductManagerController()
        {
            context = new InMemoryRepository<Product>();
            productCategories = new InMemoryRepository<ProductCategory>();
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
        public ActionResult Edit(string Id)
        {
            Product product = context.Find(Id);
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
        public ActionResult Edit(Product product, String Id)
        {
            Product productToEdit = context.Find(Id);

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

        public ActionResult Delete(string Id)
        {
            Product productToDelete = context.Find(Id);
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
        public ActionResult ConfirmDelete(string Id)
        {
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("index");            }
        }
    }
}