using Microsoft.Ajax.Utilities;
using ShoppingCart.Web.BO;
using ShoppingCart.Web.Models;
using ShoppingCart.Web.ViewModel;
using ShoppingCart.Web.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

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
        public ActionResult LoginOld(string returnurl)
        {
            ViewBag.ReturnUrl = returnurl;
            return View();
        }
        //[HttpPost]
        //public ActionResult Login(LoginViewModel model,string returnurl)
        //{
        //    if(ModelState.IsValid)
        //    {
        //        UserProfile objUserProfile = objUserProfileBO.AuthenticateUser(model.UserName);
        //        if(objUserProfile!=null)
        //        {
        //            if(model.Password==objUserProfile.Password)
        //            {
        //                string userData = objUserProfile.PKUserId + "^" + objUserProfile.FKRoleId + "^" + objUserProfile.Role.RoleName + "^" + objUserProfile.UserName + "^" + objUserProfile.FirstName + "^" + objUserProfile.LastName + "^" + objUserProfile.EmailId + "^" + objUserProfile.Password + "^" + objUserProfile.AddressLine1 + "^" + objUserProfile.AddressLine2 + "^" + objUserProfile.CreatedDate + "^" + objUserProfile.UpdatedDate;
        //                System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(userData), new string[] { userData });
        //                FormsAuthentication.RedirectFromLoginPage(Helper.UserData, model.RememberMe);
        //                objUserLoginHistory.LoginDateTime = DateTime.Now;
        //                objUserLoginHistory.FKUserId = Helper.UserId;
        //                string hostName = Dns.GetHostName();
        //                string clientIpAddress = Dns.GetHostAddresses(hostName).GetValue(1).ToString();
        //                objUserLoginHistory.IPAddress = clientIpAddress;
        //                objUserLoginHistoryBO.InsertUserLoginHistory(objUserLoginHistory);
        //                if(objUserProfile.Role.RoleName=="Admin")
        //                {
        //                    return RedirectToAction("Index", "Category", new { area = "Admin" });
        //                }
        //                else
        //                {
        //                    if(returnurl!=null)
        //                    {
        //                        return Redirect(returnurl);
        //                    }
        //                    else
        //                    {
        //                        return RedirectToAction("Index", "Home");
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                ModelState.AddModelError("", "Invalid Password");
        //                return View();
        //            }
        //        }
        //        else
        //        {
        //            ModelState.AddModelError("", "User Doesnot Exists");
        //            return View();
        //        }
        //    }
        //    return View();
        //}
        public ActionResult Register()
        {
            return View();
        }

        // GET: Account/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Account/Login
        [HttpPost]
        public ActionResult Login(LoginViewModelNew lvm)
        {
            //login
            if (ModelState.IsValid)
            {
                var appDbContext = new ApplicationDbContext();
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);
                var user = userManager.Find(lvm.Username, lvm.Password);
                //var a = appDbContext.Roles.Where(q => q.Id == user.Roles.Select(a=>a.RoleId));
                #region Just tried to add not required
                //var uRole = user.Roles.Select(y => y.RoleId).ToList();
                //string myrole = uRole[0].ToString();
                //var dbRoles1 = appDbContext.Roles.Select(u => u.Id).ToList();
                //var dbRoles4 = appDbContext.Roles.FirstOrDefault(u => u.Id == myrole);
                //var dbRoles2 = appDbContext.Roles.Where(r => r.Id == myrole).Select(n => n.Name).ToArray();
                //string role = String.Join(String.Empty, dbRoles2);  
                #endregion
                if (user != null)
                {
                    //login
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity); 
                    if (userManager.IsInRole(user.Id, "admin"))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Admin" });

                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("myerror", "Invalid username or password");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("My Error", "Invalid data");
                return View();
            }
        }

        // GET: Account/Logout
        public ActionResult Logout()
        {
            var authenticationManager = HttpContext.GetOwinContext().Authentication;
            authenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/MyProfile
        [Authorize]
        public ActionResult MyProfile()
        {
            var appDbContext = new ApplicationDbContext();
            var userStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(userStore);
            ApplicationUser appUser = userManager.FindById(User.Identity.GetUserId());
            return View(appUser);
        }
        // POST: Account/Register
        [HttpPost]
        public ActionResult Register(RegisterViewModel rvm)
        {
            if (ModelState.IsValid)
            {
                //register
                var appDbContext = new ApplicationDbContext();
                var userStore = new ApplicationUserStore(appDbContext);
                var userManager = new ApplicationUserManager(userStore);
                var passwordHash = Crypto.HashPassword(rvm.Password);
                var user = new ApplicationUser() { Email = rvm.Email, UserName = rvm.Username, PasswordHash = passwordHash, City = rvm.City, Country = rvm.Country, Address = rvm.Address, PhoneNumber = rvm.Mobile };
                IdentityResult result = userManager.Create(user);

                if (result.Succeeded)
                {
                    //role
                    userManager.AddToRole(user.Id, "Customer");

                    //login
                    var authenticationManager = HttpContext.GetOwinContext().Authentication;
                    var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                    authenticationManager.SignIn(new AuthenticationProperties(), userIdentity);
                }
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("My Error", "Invalid data");
                return View();
            }
        }

        [HttpPost]
        public ActionResult RegisterOld(UserProfile objUser)
        {
            if (ModelState.IsValid)
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