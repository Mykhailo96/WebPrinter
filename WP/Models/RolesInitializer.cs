using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WP.Models
{
    public class RolesInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            roleManager.Create(new IdentityRole("admin"));
            roleManager.Create(new IdentityRole("user"));

            var admin = new ApplicationUser { Email = "WebPrinter@gmail.com", UserName = "Admin" };
            string password = "Admin12.";
            var result = userManager.Create(admin, password);

            if (result.Succeeded)
            {
                userManager.AddToRole(admin.Id, "admin");
            }

            base.Seed(context);
        }
    }
}