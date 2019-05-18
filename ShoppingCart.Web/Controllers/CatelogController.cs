﻿using ShoppingCart.Web.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web.Controllers
{
    public class CatelogController : Controller
    {
        SubCategoryBO objSubCategoryBO = new SubCategoryBO();
        ProductBO objProductBO = new ProductBO();
        ProductImageBO objProductImageBO = new ProductImageBO();
        //
        // GET: /Catelog/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GetSubCategory(int categoryId,string categoryName)
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            return View(objSubCategoryBO.GetSubCategories(categoryId));
        }
        public ActionResult GetProducts(int categoryId,string categoryName,int subCategoryId,string subCategoryName)
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryName = subCategoryName;
            ViewBag.SubCategoryId = subCategoryId;
            return View(objProductBO.GetProducts(categoryId,subCategoryId));
        }

        public ActionResult GetAllProducts(int categoryId=0, string categoryName="", int subCategoryId=0, string subCategoryName="")
        {
            ViewBag.CategoryName = categoryName;
            ViewBag.CategoryId = categoryId;
            ViewBag.SubCategoryName = subCategoryName;
            ViewBag.SubCategoryId = subCategoryId;
            return View(objProductBO.GetProducts(categoryId, subCategoryId));
            //return PartialView("ProductsPartailView");
        }
        public ActionResult GetProductImages(int productId)
        {
            ViewBag.ProductId = productId;
            return View(objProductImageBO.GetImages(productId));
        }
        public ActionResult GetProductDetails(int productId,string imagePath)
        {
            ViewBag.ProductId = productId;
            ViewBag.Imagepath = imagePath;
            return View(objProductBO.GetProduct(productId));
        }
         
        [HttpPost,HttpGet]
        public JsonResult CreateNewCategory()
        {
            string categoryName = "ll";
            if (!string.IsNullOrEmpty(categoryName))
            {
                Category _category = new Category() { CategoryName = categoryName };
              //  objCategoryBO.InsertCategory(_category);
            }
            return Json(categoryName, JsonRequestBehavior.AllowGet);
        }

        [HttpPost, HttpGet]
        public JsonResult CreateNewCategory(string hu)
        {
            string categoryName = hu;
            if (!string.IsNullOrEmpty(categoryName))
            {
                Category _category = new Category() { CategoryName = categoryName };
                //  objCategoryBO.InsertCategory(_category);
            }
            return Json(categoryName, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ProductDetailsView()
        {
            return View();
        }

        //[HttpPost]
        //public JsonResult AddData(string ListID, string ItemName)
        //{
        //    //return Content(ListID + " " + ItemName);
        //    return Json(ItemName, JsonRequestBehavior.AllowGet);
        //}
    }
}