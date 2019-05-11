namespace BauToDoList.Migrations
{
    using BauToDoList.Models;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Threading.Tasks;

    internal sealed class Configuration : DbMigrationsConfiguration<BauToDoList.Models.appDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BauToDoList.Models.appDbContext context)
        {
            if (!context.Categories.Any())
            {
                context.Categories.Add(
                    new Models.Category()
                    {
                        Name = "Deneme",
                        CreateDate = DateTime.Now,
                        CreatedBy = "username",
                        UpdateDate = DateTime.Now,
                        UpdatedBy = "username"
                    }
                    );
                var store = new UserStore<ApplicationUser>(context);
                var manager = new ApplicationUserManager(store);
                var user = new ApplicationUser()
                {
                    Email = "muhamed.genco@gmail.com",
                    UserName = "muhamed.genco@gmail.com"
                };
                Task<Microsoft.AspNet.Identity.IdentityResult> task = Task.Run(() => manager.CreateAsync(user, "@Genco1234"));

                var result = task.Result;
                context.SaveChanges();
            }
            //  This method will be called after migrating to the latest version.
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}
