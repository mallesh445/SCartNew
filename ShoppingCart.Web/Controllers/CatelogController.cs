using ShoppingCart.Web.BO;
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
	}
}