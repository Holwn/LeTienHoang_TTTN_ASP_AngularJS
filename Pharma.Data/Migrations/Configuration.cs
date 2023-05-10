namespace Pharma.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Pharma.Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Pharma.Data.PharmaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Pharma.Data.PharmaDbContext context)
        {
            CreateProductCategorySample(context);
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
            //var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new PharmaDbContext()));

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new PharmaDbContext()));

            //var user = new ApplicationUser()
            //{
            //    UserName = "hoang",
            //    Email = "letienhoang000@gmail.com",
            //    EmailConfirmed = true,
            //    Birthday = DateTime.Now,
            //    FullName = "Le Tien Hoang"
            //};

            //manager.Create(user, "123456$");

            //if(!roleManager.Roles.Any())
            //{
            //    roleManager.Create(new IdentityRole { Name = "Admin" });
            //    roleManager.Create(new IdentityRole { Name = "User" });
            //}

            //var adminUser = manager.FindByEmail("letienhoang000@gmail.com");

            //manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });
        }

        private void CreateProductCategorySample(Pharma.Data.PharmaDbContext context)
        {
            if(context.ProductCategories.Count()==0)
            {
                List<ProductCategory> listProductCategory = new List<ProductCategory>() {
                new ProductCategory(){Name="Dạng thuốc uống",Alias="dang-thuoc-uong",Status=true},
                new ProductCategory(){Name="Dạng thuốc tiêm",Alias="dang-thuoc-tiem",Status=true},
                new ProductCategory(){Name="Dạng thuốc dùng ngoài da",Alias="dang-thuoc-dung-ngoai-da",Status=true},
                new ProductCategory(){Name="Dạng thuốc đặt vào các hốc tự nhiên trên cơ thể ",Alias="dang-thuoc-dat-vao-cac-hoc-tu-nhien-tren-co-the",Status=true}
                };
                context.ProductCategories.AddRange(listProductCategory);
                context.SaveChanges();
            }    
            
        }
    }
}
