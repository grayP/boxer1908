namespace bw01.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using System.Data.Entity.Migrations;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<bw01.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "bw01.Models.ApplicationDbContext";
        }

        private void SeedUsers()
        {

            string _role = "Admin";
            if (!Roles.RoleExists(_role))
                Roles.CreateRole(_role);

            if (!Roles.IsUserInRole("gray.pritchett@optusnet.com.au", _role))
                Roles.AddUserToRole("gray.pritchett@optusnet.com.au", _role);
            if (!Roles.IsUserInRole("ctyquin@goa.com.au", _role))
                Roles.AddUserToRole("ctyquin@goa.com.au", _role);
            if (!Roles.IsUserInRole("GClarke@bne.mcgees.com.au", _role))
                Roles.AddUserToRole("GClarke@bne.mcgees.com.au", _role);
            if (!Roles.IsUserInRole("alex@propsol.com.au", _role))
                Roles.AddUserToRole("alex@propsol.com.au", _role);
            if (!Roles.IsUserInRole("pcrooke@bretts.com.au", _role))
                Roles.AddUserToRole("pcrooke@bretts.com.au", _role);
            if (!Roles.IsUserInRole("fraser@rsaarchitects.net", _role))
                Roles.AddUserToRole("fraser@rsaarchitects.net", _role);
        }



        protected override void Seed(bw01.Models.ApplicationDbContext context)
        {
            SeedUsers();

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



            var user = new ApplicationUser()
            {
                UserName = "gray.pritchett@optusnet.com.au",
                Email= "gray.pritchett@optusnet.com.au"
            };

            ir = um.Create(user, "P@ssword1");
            //if (ir.Succeeded == false)
            //    return ir.Succeeded;
            ir = um.AddToRole(user.Id, "Admin");

            user = new ApplicationUser()
            {
                UserName = "ctyquin@goa.com.au",
                Email= "ctyquin@goa.com.au",
            };

            ir = um.Create(user, "P@ssword1");
            //if (ir.Succeeded == false)
            //    return ir.Succeeded;
            ir = um.AddToRole(user.Id, "Admin");

            user = new ApplicationUser()
            {
                UserName = "GClarke@bne.mcgees.com.au",
                Email= "GClarke@bne.mcgees.com.au",
            };

            ir = um.Create(user, "P@ssword1");
            //if (ir.Succeeded == false)
            //    return ir.Succeeded;
            ir = um.AddToRole(user.Id, "Admin");

            user = new ApplicationUser()
            {
                UserName = "alex@propsol.com.au",
                Email = "alex@propsol.com.au",
            };

            ir = um.Create(user, "P@ssword1");
            //if (ir.Succeeded == false)
            //    return ir.Succeeded;
            ir = um.AddToRole(user.Id, "Admin");

            user = new ApplicationUser()
            {
                UserName = "pcrooke@bretts.com.au",
                Email= "pcrooke@bretts.com.au",
            };

            ir = um.Create(user, "P@ssword1");

            ir = um.AddToRole(user.Id, "Admin");

            
            user = new ApplicationUser()
            {
                UserName = "fraser@rsaarchitects.net",
                Email = "fraser@rsaarchitects.net",
            };

            ir = um.Create(user, "P@ssword1");

            ir = um.AddToRole(user.Id, "Admin");

            return ir.Succeeded;

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

