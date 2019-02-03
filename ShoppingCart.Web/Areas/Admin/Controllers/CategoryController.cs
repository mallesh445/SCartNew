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
    //[Authorize]
    public class CategoryController : Controller
    {
        CategoryBO objCategoryBO = new CategoryBO();
        // GET: /Admin/Category/
        public ActionResult Index()
        {
            return View(objCategoryBO.GetCategories());
        }

        // GET: /Admin/Category/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = objCategoryBO.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: /Admin/Category/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Category/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                objCategoryBO.InsertCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Admin/Category/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = objCategoryBO.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Admin/Category/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                objCategoryBO.UpdateCategory(category);
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: /Admin/Category/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = objCategoryBO.GetCategory(id.Value);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: /Admin/Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objCategoryBO.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
