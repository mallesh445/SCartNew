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
    public class ProductImageController : Controller
    {

        ProductImageBO objProductImageBO = new ProductImageBO();
        // GET: /Admin/ProductImage/
        public ActionResult Index(int? id)
        {
            TempData["productId"] = id.Value;
            TempData.Keep();
            return View(objProductImageBO.GetImages(id.Value));
        }

        // GET: /Admin/ProductImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/ProductImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductImage productimage, IEnumerable<HttpPostedFileBase> images)
        {
            if (ModelState.IsValid)
            {
                foreach (HttpPostedFileBase img in images)
                {
                    string imageName = System.IO.Path.GetFileName(img.FileName);
                    img.SaveAs(Server.MapPath("~/Images/" + imageName));
                    productimage.FKProductId = (int)TempData["productId"];
                    productimage.ImageName = imageName;
                    productimage.ImagePath = "/Images/" + imageName;
                    objProductImageBO.InsertImage(productimage);
                }
                return RedirectToAction("Index", new {id=productimage.FKProductId });
            }
            return View(productimage);
        }

        // GET: /Admin/ProductImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productimage = objProductImageBO.GetImage(id.Value);
            if (productimage == null)
            {
                return HttpNotFound();
            }
            return View(productimage);
        }

        // POST: /Admin/ProductImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProductImage productimage,HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                string imageName = System.IO.Path.GetFileName(image.FileName);
                image.SaveAs(Server.MapPath("~/Images/" + imageName));
                productimage.ImageName = imageName;
                productimage.ImagePath = "/Images/" + imageName;
                objProductImageBO.UpdateImage(productimage);
            }
            return RedirectToAction("Index", new { id = productimage.FKProductId });
        }

        // GET: /Admin/ProductImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductImage productimage = objProductImageBO.GetImage(id.Value);
            if (productimage == null)
            {
                return HttpNotFound();
            }
            return View(productimage);
        }

        // POST: /Admin/ProductImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objProductImageBO.DeleteImage(id);
            int productId = Convert.ToInt32(TempData["productId"]);
            return RedirectToAction("Index", new { id = productId });
        }
    }
}
