using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShoppingCart.Web;
using ShoppingCart.Web.BO;

namespace ShoppingCart.Web.Areas.Admin.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        ProductBO objProductBO = new ProductBO();
        CategoryBO objCategoryBO = new CategoryBO();
        SubCategoryBO objSubCategoryBO = new SubCategoryBO();
        ProductImageBO objProductImageBO = new ProductImageBO();

        // GET: /Admin/Product/
        public ActionResult Index()
        {
            ViewBag.Category = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
            ViewBag.SubCategory = new SelectList(objSubCategoryBO.GetSubCategories(0, true), "PKSubCategoryId", "SubCategoryName");
            return View(objProductBO.GetProducts());
        }

        public ActionResult List()
        {
            return View(objProductBO.GetProducts());
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            if (fc["btnSearch"] == "Search")
            {
                int category = 0;
                int subCategory = 0;
                if (fc["Category"] != "")
                    category = Convert.ToInt32(fc["Category"]);
                if (fc["SubCategory"] != "")
                    subCategory = Convert.ToInt32(fc["SubCategory"]);
                ViewBag.Category = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName", category);
                ViewBag.SubCategory = new SelectList(objSubCategoryBO.GetSubCategories(0, true), "PKSubCategoryId", "SubCategoryName", subCategory);
                return View(objProductBO.GetProducts(category, subCategory));
            }
            else
            {
                ViewBag.Category = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
                ViewBag.SubCategory = new SelectList(objSubCategoryBO.GetSubCategories(0, true), "PKSubCategoryId", "SubCategoryName");
                return View(objProductBO.GetProducts());
            }
        }
        // GET: /Admin/Product/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = objProductBO.GetProduct(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        public ActionResult GetSubCategories(int id = 0)
        {
            var lstSubCategories = objSubCategoryBO.GetSubCategories(id);
            return Json(lstSubCategories, JsonRequestBehavior.AllowGet);
        }

        // GET: /Admin/Product/Create
        public ActionResult Create()
        {
            ViewBag.FKCategoryId = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
            ViewBag.FKSubCategoryId = new SelectList(objSubCategoryBO.GetSubCategories(0, true), "PKSubCategoryId", "SubCategoryName");
            return View();
        }

        // POST: /Admin/Product/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product, IEnumerable<HttpPostedFileBase> images)
        {
            ProductImage objProductImage = new ProductImage();
            if (ModelState.IsValid)
            {
                objProductBO.InsertProduct(product);
                foreach (HttpPostedFileBase img in images)
                {
                    string imageName = System.IO.Path.GetFileName(img.FileName);
                    img.SaveAs(Server.MapPath("~/Images/" + imageName));
                    objProductImage.FKProductId = product.PKProductId;
                    objProductImage.ImageName = imageName;
                    objProductImage.ImagePath = "/Images/" + imageName;
                    objProductImageBO.InsertImage(objProductImage);
                }
                return RedirectToAction("Index");
            }

            ViewBag.FKCategoryId = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
            ViewBag.FKSubCategoryId = new SelectList(objSubCategoryBO.GetSubCategories(0, true), "PKSubCategoryId", "SubCategoryName");
            return View(product);
        }

        // GET: /Admin/Product/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = objProductBO.GetProduct(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.FKCategoryId = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName", product.FKCategoryId);
            ViewBag.FKSubCategoryId = new SelectList(objSubCategoryBO.GetSubCategories(0, true), "PKSubCategoryId", "SubCategoryName", product.FKSubCategoryId);
            return View(product);
        }

        // POST: /Admin/Product/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product product, FormCollection fc)
        {
            if (ModelState.IsValid)
            {
                objProductBO.UpdateProduct(product);
                if (fc["btnEdit"] == "Edit product images")
                {
                    return RedirectToAction("Index", "Productimage", new { id = product.PKProductId });
                }
                return RedirectToAction("Index");
            }
            ViewBag.FKCategoryId = new SelectList(objCategoryBO.GetCategories(true), "PKCategoryId", "CategoryName");
            ViewBag.FKSubCategoryId = new SelectList(objSubCategoryBO.GetSubCategories(0, true), "PKSubCategoryId", "SubCategoryName");
            return View(product);
        }

        // GET: /Admin/Product/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = objProductBO.GetProduct(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: /Admin/Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            List<ProductImage> lstProductImage = objProductImageBO.GetImages(id);
            foreach (ProductImage p in lstProductImage)
            {
                objProductImageBO.DeleteImage(p.PKImageId);
            }
            objProductBO.DeleteProduct(id);
            return RedirectToAction("Index");
        }
    }
}
