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
    public class ErrorHistoryController : Controller
    {
        ErrorHistoryBO objErrorHistoryBO = new ErrorHistoryBO();

        // GET: /Admin/Error/
        public ActionResult Index()
        {
            return View(objErrorHistoryBO.GetErrorHistories());
        }

        // GET: /Admin/Error/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorHistory errorhistory = objErrorHistoryBO.GetErrorHistory(id.Value);
            if (errorhistory == null)
            {
                return HttpNotFound();
            }
            return View(errorhistory);
        }

        // GET: /Admin/Error/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Error/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ErrorHistory errorhistory)
        {
            if (ModelState.IsValid)
            {
                objErrorHistoryBO.InsertErrorHistory(errorhistory);
                return RedirectToAction("Index");
            }
            return View(errorhistory);
        }

        // GET: /Admin/Error/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorHistory errorhistory = objErrorHistoryBO.GetErrorHistory(id.Value);
            if (errorhistory == null)
            {
                return HttpNotFound();
            }
            return View(errorhistory);
        }

        // POST: /Admin/Error/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(ErrorHistory errorhistory)
        {
            if (ModelState.IsValid)
            {
                objErrorHistoryBO.UpdateErrorHistory(errorhistory);
                return RedirectToAction("Index");
            }
            return View(errorhistory);
        }

        // GET: /Admin/Error/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ErrorHistory errorhistory = objErrorHistoryBO.GetErrorHistory(id.Value);
            if (errorhistory == null)
            {
                return HttpNotFound();
            }
            return View(errorhistory);
        }

        // POST: /Admin/Error/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objErrorHistoryBO.DeleteErrorHistory(id);
            return RedirectToAction("Index");
        }
    }
}
