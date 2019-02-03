using ShoppingCart.Web.BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web.Controllers
{
    [Authorize]
    public class ConfirmController : Controller
    {
        BillingAddressBO objBillingAddressBO = new BillingAddressBO();
        ProductBO objProductBO = new ProductBO();
        Order objOrder = new Order();
        OrderBO objOrderBO = new OrderBO();
        OrderDetail objOrderDetail = new OrderDetail();
        OrderDetailBO objOrderDetailBO = new OrderDetailBO();
        OrderHistory objOrderHistory = new OrderHistory();
        OrderHistoryBO objOrderHistoryBO = new OrderHistoryBO();

        //
        // GET: /Confirm/dd
        public ActionResult PlaceOrder()
        {
            return View();
        }
        [HttpPost, ActionName("PlaceOrder")]
        public ActionResult ConfirmOrder(BillingAddress objBillingAddress)
        {
            objBillingAddressBO.InsertBillingAddress(objBillingAddress);
            int addressId = objBillingAddress.PKAddressId;
            HttpCookie CartCookie = Request.Cookies[Helper.UserName];
            foreach (var items in CartCookie.Values)
            {
                string cookieValue = CartCookie[items.ToString()].ToString();
                var product = objProductBO.GetProduct(Convert.ToInt32(items));
                objOrder.FKProductId = product.PKProductId;
                objOrder.FKBillingAddressId = addressId;
                objOrder.Status = "Processing";
                objOrder.CustomerComment = cookieValue.Split('^')[2];
                objOrderBO.InsertOrder(objOrder);
                objOrderDetail.FKProductId = product.PKProductId;
                objOrderDetail.FKOrderId = objOrder.PKOrderId;
                objOrderDetail.Cost = Convert.ToDecimal(cookieValue.Split('^')[1]);
                objOrderDetail.Quantity = Convert.ToInt32(cookieValue.Split('^')[0]);
                objOrderDetailBO.InsertOrderDetail(objOrderDetail);
                objOrderHistory.FKOrderId = objOrder.PKOrderId;
                objOrderHistory.Status = objOrder.Status;
                objOrderHistoryBO.InsertOrderHistory(objOrderHistory);
            }
            CartCookie.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(CartCookie);
            return RedirectToAction("GetOrderDetails", "MyOrder");
        }
    }
}