using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShoppingCart.Web.Controllers
{
    public class HomeController : Controller
    {
        //1^1^Admin^Admin^Ad^min^Admin@Store.Com^Admin^Roadno12^qwerty^03-05-2016 00:00:00^
        // GET: /Home/
        public ActionResult Index()
        { 
            string userData = System.Web.HttpContext.Current.User.Identity.Name;
            if (userData != null && userData != "")
            {
                string roleName= userData.Split('^')[2];
                if (string.Equals(roleName,"admin",StringComparison.OrdinalIgnoreCase))
                {
                    return RedirectToAction("Index", "Category", new { area = "Admin" });
                }
                else
                {
                    return View();
                }
            }
            else
            {
                return View();
            }
        }
        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }

	}
}