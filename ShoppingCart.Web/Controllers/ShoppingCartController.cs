using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web.Controllers
{
    [Authorize]
    public class ShoppingCartController : Controller
    {
        //
        // GET: /ShowCart
        HttpCookie CartCookie;
        public ActionResult AddToCart(int? productId=0, decimal? price=0, int? quantity=0)
        {
            if (productId != 0)
            {
                CartCookie = Request.Cookies[Helper.UserName];
                if (CartCookie == null)
                {
                    CartCookie = new HttpCookie(Helper.UserName);
                    CartCookie.Expires = DateTime.MaxValue;
                }
                quantity = 1;
                CartCookie.Values[productId.ToString()] = quantity.ToString() + '^' + price.ToString();
                Response.Cookies.Add(CartCookie);
            }
            return RedirectToAction("ShowCart");
        }
        public ActionResult GetShowCartDetails(int id, int total, int qty, string customerComment)
         {
            CartCookie = Request.Cookies[Helper.UserName];
            CartCookie.Values[id.ToString()] = qty.ToString() + '^' + total.ToString() + '^' + customerComment;
            Response.Cookies.Add(CartCookie);
            return RedirectToAction("ShowCart");
        }
        public ActionResult ShowCart()
        {
            return View();
        }
        public ActionResult ShowCartProductRemove(int deleteId)
        {
            ViewBag.Id = Helper.UserId;
            HttpCookie CartCookie = Request.Cookies[Helper.UserName];
            CartCookie.Values.Remove(deleteId.ToString());
            Response.Cookies.Add(CartCookie);
            return RedirectToAction("ShowCart");
        }
    }
}