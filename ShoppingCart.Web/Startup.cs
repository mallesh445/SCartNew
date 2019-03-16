using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity.EntityFramework;
using ShoppingCart.Web.Identity;
using Microsoft.AspNet.Identity.Owin;

[assembly: OwinStartup(typeof(ShoppingCart.Web.Startup))]

namespace ShoppingCart.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            app.UseCookieAuthentication(new CookieAuthenticationOptions()
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                },
                ExpireTimeSpan = TimeSpan.FromMinutes(5)
            });

            this.CreateRolesAndUsers();
            //app.usegoo(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "dsdsd",
            //    ClientSecret = "dsds"
            //});
        }

        private void CreateRolesAndUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>());
            var appDbContext = new ApplicationDbContext();
            var appUserStore = new ApplicationUserStore(appDbContext);
            var userManager = new ApplicationUserManager(appUserStore);

            //Create Admin Role
            if (!roleManager.RoleExists("Admin"))
            {
                var role = new IdentityRole();
                role.Name = "Admin";
                roleManager.Create(role);
            }

            //Create Admin User
            if (userManager.FindByName("Admin") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "admin";
                user.Email = "admin@gmail.com";
                string password = "admin123";
                var chkUser = userManager.Create(user, password);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Admin");
                }
            }

            //Create Manager Role
            if (!roleManager.RoleExists("Manager"))
            {
                var role = new IdentityRole();
                role.Name = "Manager";
                roleManager.Create(role);
            }

            //Create Manager User
            if (userManager.FindByName("manager") == null)
            {
                var user = new ApplicationUser();
                user.UserName = "manager";
                user.Email = "manager@gmail.com";
                string password = "manager123";
                var chkUser = userManager.Create(user, password);
                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, "manager");
                }
            }

            //Create Customer Role
            if (!roleManager.RoleExists("customer"))
            {
                var role = new IdentityRole();
                role.Name = "customer";
                roleManager.Create(role);
            }

            ////Create Customer User
            //if (userManager.FindByName("customer") == null)
            //{
            //    var user = new ApplicationUser();
            //    user.UserName = "customer";
            //    user.Email = "customer@gmail.com";
            //    string password = "customer123";
            //    var chkUser = userManager.Create(user, password);
            //    if (chkUser.Succeeded)
            //    {
            //        userManager.AddToRole(user.Id, "customer");
            //    }
            //}

        }
    }
}
