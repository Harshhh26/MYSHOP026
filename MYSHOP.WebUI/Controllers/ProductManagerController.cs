using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MYSHOP.CORE.Models;
using MYSHOP.CORE.ViewModels;
using MYSHOP.CORE.Contracts;
using System.IO;
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
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (!ModelState.IsValid) 
            {
                return View(product);
            }    
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("~/Content/ProductImage/") + product.Image);
                }

                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(String Id)
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
        public ActionResult Edit(Product product, String Id, HttpPostedFileBase file )
        {
            Product productToEdit = context.Find(Id);

            if (productToEdit == null)
            {
                return HttpNotFound();
            }
            else
            {
                if (!ModelState.IsValid)
                {
                    return View(product);
                }
                if (file != null)
                {
                    productToEdit.Image = productToEdit.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("~/Content/ProductImage") + productToEdit.Image);
                }

                productToEdit.Category = product.Category;
                
                productToEdit.Depriciation = product.Depriciation;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;

                context.Commit();

                return RedirectToAction("Index"); 
            }
        }

        public ActionResult Delete(String Id)
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
        public ActionResult ConfirmDelete(String Id)
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