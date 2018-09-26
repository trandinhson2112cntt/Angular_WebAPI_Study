namespace App.Data.Migrations
{
    using App.Model.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<App.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(App.Data.AppDbContext context)
        {
            var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new AppDbContext()));

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new AppDbContext()));

            var user = new ApplicationUser()
            {
                UserName = "death",
                Email = "trandinhson2112cntt@gmail.com",
                EmailConfirmed = true,
                BirthDay = DateTime.Now,
                FullName = "Tran Dinh Son"
            };

            manager.Create(user, "123456$");

            if (!roleManager.Roles.Any())
            {
                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "User" });
            }

            var adminUser = manager.FindByEmail("trandinhson2112cntt@gmail.com");

            manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
            CreateProductCategorySample(context);
        }

        private void CreateProductCategorySample(AppDbContext context)
        {
            if (context.ProductCategories.Count() == 0)
            {
                List<ProductCategory> listProductCategories = new List<ProductCategory>()
            {
                new ProductCategory()
                {
                    Name = "Điện lạnh",
                    Alias = "dien-lanh",
                    Status = true
                },
                new ProductCategory()
                {
                    Name = "Viễn thông",
                    Alias = "vien-thong",
                    Status = true
                },
                new ProductCategory()
                {
                    Name = "Mỹ phẩm",
                    Alias = "my-pham",
                    Status = true
                }
            };
                context.ProductCategories.AddRange(listProductCategories);
                context.SaveChanges();
            }
        }
    }
}