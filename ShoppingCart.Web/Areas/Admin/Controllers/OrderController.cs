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
    public class OrderController : Controller
    {
        OrderBO objOrderBO = new OrderBO();
        OrderHistory objOrderHistory = new OrderHistory();
        OrderHistoryBO objOrderHistoryBO = new OrderHistoryBO();
        // GET: /Admin/Order/
        public ActionResult Index()
        {
            return View(objOrderBO.GetOrders());
        }
        [HttpPost]
        public ActionResult Index(FormCollection fc)
        {
            DateTime fromDate;
            DateTime toDate;
            string userName;
            string status;
            if (fc["txtFromDate"] == "")
            {
                userName = fc["txtUserName"];
                status = fc["Status"];
                return View(objOrderBO.GetOrders(null, null, userName, status));
            }

            else if (fc["btnSearch"] == "Search")
            {
                fromDate = DateTime.Parse(fc["txtFromDate"]);
                toDate = DateTime.Parse(fc["txtToDate"]);
                userName = fc["txtUserName"];
                status = fc["Status"];
                return View(objOrderBO.GetOrders(fromDate, toDate, userName, status));
            }
            else
            {
                return View(objOrderBO.GetOrders());
            }
        }

        // GET: /Admin/Order/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = objOrderBO.GetOrder(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: /Admin/Order/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Admin/Order/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Order order)
        {
            if (ModelState.IsValid)
            {
                objOrderBO.InsertOrder(order);
                return RedirectToAction("Index");
            }

            return View(order);
        }

        // GET: /Admin/Order/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = objOrderBO.GetOrder(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            else
            {
                IEnumerable<Status> values = Enum.GetValues(typeof(Status)).Cast<Status>();
                IEnumerable<SelectListItem> items = from value in values
                                                    select new SelectListItem
                                                    {
                                                        Text = value.ToString(),
                                                        Value = value.ToString(),
                                                    };
                ViewBag.StatusData = items;
            }
            return View(order);

        }

        // POST: /Admin/Order/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Order order)
        {
            if (ModelState.IsValid)
            {
                objOrderBO.UpdateOrder(order);
                objOrderHistory = objOrderHistoryBO.GetOrderHistory(order.PKOrderId);
                if (objOrderHistory != null)
                {
                    objOrderHistory.Status = order.Status;
                    objOrderHistory.Description = order.AdminComment;
                    objOrderHistoryBO.UpdateOrderHistory(objOrderHistory);
                }
                return RedirectToAction("Index");
            }
            return View(order);
        }

        // GET: /Admin/Order/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = objOrderBO.GetOrder(id.Value);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: /Admin/Order/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            objOrderBO.DeleteOrder(id);
            return RedirectToAction("Index");
        }
        public JsonResult GetOrderDetails(int id)
        {
            OrderDetailBO objOrderDetailBO = new OrderDetailBO();
            List<OrderDetail> lstOrderdetails = objOrderDetailBO.GetOrderDetails(id);
            return Json(lstOrderdetails, JsonRequestBehavior.AllowGet);
        }
    }
}
