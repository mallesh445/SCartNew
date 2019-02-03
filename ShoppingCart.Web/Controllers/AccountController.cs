using Microsoft.Ajax.Utilities;
using ShoppingCart.Web.BO;
using ShoppingCart.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShoppingCart.Web.Controllers
{
    public class AccountController : Controller
    {
        UserProfileBO objUserProfileBO = new UserProfileBO();
        UserLoginHistory objUserLoginHistory = new UserLoginHistory();
        UserLoginHistoryBO objUserLoginHistoryBO = new UserLoginHistoryBO();
        //
        // GET: /Account/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginViewModel model,string returnurl)
        {
            if(ModelState.IsValid)
            {
                UserProfile objUserProfile = objUserProfileBO.AuthenticateUser(model.UserName);
                if(objUserProfile!=null)
                {
                    if(model.Password==objUserProfile.Password)
                    {
                        string userData = objUserProfile.PKUserId + "^" + objUserProfile.FKRoleId + "^" + objUserProfile.Role.RoleName + "^" + objUserProfile.UserName + "^" + objUserProfile.FirstName + "^" + objUserProfile.LastName + "^" + objUserProfile.EmailId + "^" + objUserProfile.Password + "^" + objUserProfile.AddressLine1 + "^" + objUserProfile.AddressLine2 + "^" + objUserProfile.CreatedDate + "^" + objUserProfile.UpdatedDate;
                        System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(userData), new string[] { userData });
                        FormsAuthentication.RedirectFromLoginPage(Helper.UserData, model.RememberMe);
                        objUserLoginHistory.LoginDateTime = DateTime.Now;
                        objUserLoginHistory.FKUserId = Helper.UserId;
                        string hostName = Dns.GetHostName();
                        string clientIpAddress = Dns.GetHostAddresses(hostName).GetValue(1).ToString();
                        objUserLoginHistory.IPAddress = clientIpAddress;
                        objUserLoginHistoryBO.InsertUserLoginHistory(objUserLoginHistory);
                        if(objUserProfile.Role.RoleName=="Admin")
                        {
                            return RedirectToAction("Index", "Category", new { area = "Admin" });
                        }
                        else
                        {
                            if(returnurl!=null)
                            {
                                return Redirect(returnurl);
                            }
                            else
                            {
                                return RedirectToAction("Index", "Home");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Invalid Password");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("", "User Doesnot Exists");
                    return View();
                }
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserProfile objUser)
        {
            if(ModelState.IsValid)
            {
                objUser.FKRoleId = 2;
                objUserProfileBO.InsertUser(objUser);
                return RedirectToAction("Login");
            }
            return View(User);
        }
        public JsonResult IsUserNameValid(string userName)
        {
            if (objUserProfileBO.AuthenticateUser(userName) != null)
            {
                return Json("Sorry! UserName Already Exist", JsonRequestBehavior.AllowGet);

            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            //return RedirectToRoute("Default");
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public ActionResult ManageUserProfile()
        {
            return View();
        }
	}
}