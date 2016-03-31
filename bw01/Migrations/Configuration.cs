namespace bw01.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<bw01.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "bw01.Models.ApplicationDbContext";
        }

        protected override void Seed(bw01.Models.ApplicationDbContext context)
        {
            AddUserAndRole(context);
                }


            bool AddUserAndRole(bw01.Models.ApplicationDbContext context)
        {
                IdentityResult ir;
                var rm = new RoleManager<IdentityRole>
                    (new RoleStore<IdentityRole>(context));
                ir = rm.Create(new IdentityRole("Admin"));
                var um = new UserManager<ApplicationUser>(
                    new UserStore<ApplicationUser>(context));


                if (!Roles.IsUserInRole("gray.pritchett@optusnet.com.au", "Admin"))
                {


                    ir = um.AddToRole("gray.pritchett@optusnet.com.au", "canEdit");
                    return ir.Succeeded;
                }
                else
                {
                    return false;
                }
            }
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
