﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Store.Core.Contracts;
using Store.Core.Models;
using Store.Core.ViewModels;


namespace Store.WebUI.Controllers
{
    [Authorize(Roles = RoleName.AskAdmin )]
    public class ProductManagerController : Controller
    {
        IRepository<Product> context;
        IRepository<ProductCategory> productCategories;

        public ProductManagerController(IRepository<Product> productContext, IRepository<ProductCategory> productCategoryContext)
        {
            context = productContext;
            productCategories = productCategoryContext;
        }
        // GET: ProductManager
        public ActionResult Index()
        {
            ViewBag.IsIndexHome = false;
            List<Product> products = context.Collection().ToList();
            return View(products);
        }
        public ActionResult Create()
        {
            ViewBag.IsIndexHome = false;
            ProductManagerViewModel viewModel = new ProductManagerViewModel
            {
                Product = new Product(),
                ProductCategories = productCategories.Collection()
            };

            return View(viewModel);
        }
        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            ViewBag.IsIndexHome = false;
            if (!ModelState.IsValid)
            {
                return View(product);

            }
            else
            {
                if (file != null)
                {
                    product.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + product.Image);
                }
                context.Insert(product);
                context.Commit();

                return RedirectToAction("Index");
            }
        }
        public ActionResult Edit(string Id)
        {
            ViewBag.IsIndexHome = false;
            Product product = context.Find(Id);
            if (product == null)
            {
                return HttpNotFound();
            }
            else
            {
                ProductManagerViewModel viewModel = new ProductManagerViewModel
                {
                    Product = product,
                    ProductCategories = productCategories.Collection()
                };

                return View(viewModel);
            }
        }
        [HttpPost]
        public ActionResult Edit(Product product, string Id, HttpPostedFileBase file,HttpPostedFileBase file2, HttpPostedFileBase file3)
        {
            ViewBag.IsIndexHome = false;
            Product productToEdit = context.Find(Id);
            if (product == null)
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
                    productToEdit.Image = product.Id + Path.GetExtension(file.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image);
                }

                if (file != null)
                {
                    productToEdit.Image2 = product.Id + Path.GetExtension(file2.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image2);
                }

                if (file3 != null)
                {
                    productToEdit.Image3 = product.Id + Path.GetExtension(file3.FileName);
                    file.SaveAs(Server.MapPath("//Content//ProductImages//") + productToEdit.Image3);
                }

                productToEdit.Category = product.Category;
                productToEdit.Description = product.Description;
                productToEdit.Name = product.Name;
                productToEdit.Price = product.Price;
                productToEdit.Shipping = product.Shipping;

                context.Commit();

                return RedirectToAction("Index");
            }

        }

        public ActionResult Details(string Id)
        {
            ViewBag.IsIndexHome = false;
            Product product = context.Find(Id);
            if(product == null)
            {
                return HttpNotFound();
            }
            else
            {
                return View(product);
            }

        }
        public ActionResult Delete(string Id)
        {
            ViewBag.IsIndexHome = false;
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
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
            ViewBag.IsIndexHome = false;
            Product productToDelete = context.Find(Id);
            if (productToDelete == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete(Id);
                context.Commit();
                return RedirectToAction("Index");
            }

        }
       
    }
}