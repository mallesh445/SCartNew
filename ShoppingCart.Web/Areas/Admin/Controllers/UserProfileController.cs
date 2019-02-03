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
    public class UserProfileController : Controller
    {
        UserProfileBO objUserProfileBO = new UserProfileBO();

        // GET: /Admin/UserProfile/
        public ActionResult Index()
        {
            return View(objUserProfileBO.GetUsers());
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            string emailId = fc["txtEmailId"];
            string phoneNo = fc["txtPhoneNo"];
            DateTime? fromDate = null;
            DateTime? toDate = null;
            if (!string.IsNullOrEmpty(Convert.ToString(fc["txtFromDate"])))
            {
                fromDate = DateTime.Parse(Convert.ToString(fc["txtFromDate"]));
            }
            if (!string.IsNullOrEmpty(Convert.ToString(fc["txtToDate"])))
            {
                toDate = DateTime.Parse(Convert.ToString(fc["txtToDate"]));
            }
            if (fc["btnSearch"] == "Search")
            {
                return View(objUserProfileBO.GetUsers(emailId, phoneNo, fromDate, toDate));
            }
            else
            {
                return View();
            }
        }

        // GET: /Admin/UserProfile/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = objUserProfileBO.GetUser(id.Value);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // GET: /Admin/UserProfile/Create
        public ActionResult Create()
        {
            //ViewBag.FKRoleId = new SelectList(db.Roles, "PKRoleId", "RoleName");
            //ViewBag.CreatedByUserId = new SelectList(db.UserProfiles, "PKUserId", "UserName");
            //ViewBag.UpdatedByUserId = new SelectList(db.UserProfiles, "PKUserId", "UserName");
            return View();
        }

        // POST: /Admin/UserProfile/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                objUserProfileBO.InsertUser(userprofile);
                return RedirectToAction("Index");
            }
            return View(userprofile);
        }

        // GET: /Admin/UserProfile/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = objUserProfileBO.GetUser(id.Value);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /Admin/UserProfile/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserProfile userprofile)
        {
            if (ModelState.IsValid)
            {
                objUserProfileBO.UpdateUser(userprofile);
                return RedirectToAction("Index");
            }
            //ViewBag.FKRoleId = new SelectList(db.Roles, "PKRoleId", "RoleName", userprofile.FKRoleId);
            //ViewBag.CreatedByUserId = new SelectList(db.UserProfiles, "PKUserId", "UserName", userprofile.CreatedByUserId);
            //ViewBag.UpdatedByUserId = new SelectList(db.UserProfiles, "PKUserId", "UserName", userprofile.UpdatedByUserId);
            return View(userprofile);
        }

        // GET: /Admin/UserProfile/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserProfile userprofile = objUserProfileBO.GetUser(id.Value);
            if (userprofile == null)
            {
                return HttpNotFound();
            }
            return View(userprofile);
        }

        // POST: /Admin/UserProfile/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objUserProfileBO.DeleteUser(id);
            return RedirectToAction("Index");
        }
        public ActionResult UserLoginHistory(int id)
        {
            UserLoginHistoryBO objUserLoginHistoryBO = new UserLoginHistoryBO();
            IEnumerable<UserLoginHistory> user= objUserLoginHistoryBO.GetUserLoginHistories(id);
            return View(user);
        }
        public ActionResult OrderHistory(int? id=0)
        {
            OrderHistoryBO objOrderHistoryBO = new OrderHistoryBO();
            objOrderHistoryBO.GetOrderHistories(id.Value);
            return View("Index");
        }
    }
}
