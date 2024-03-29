﻿using Microsoft.AspNet.Identity.EntityFramework;
using Pharma.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Data
{
    public class PharmaDbContext : IdentityDbContext<ApplicationUser>
    {
        public PharmaDbContext() : base("PharmaConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<ContactDetail> ContactDetails { set; get; }
        public DbSet<DeliveryNote> DeliveryNotes { set; get; }
        public DbSet<DeliveryNoteItem> DeliveryNoteItems { set; get; }
        public DbSet<Feedback> Feedbacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
        public DbSet<NoteBook> NoteBooks { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Post> Posts { set; get; }
        public DbSet<PostCategory> PostCategories { set; get; }
        public DbSet<PostTag> PostTags { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductMapping> ProductMappings { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<ReceiptNote> ReceiptNotes { set; get; }
        public DbSet<ReceiptNoteItem> ReceiptNoteItems { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<Store> Stores { set; get; }
        public DbSet<StoreRole> StoreRoles { set; get; }
        public DbSet<Subject> Subjects { set; get; }
        public DbSet<SupportOnline> SupportOnlines { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<Unit> Units { set; get; }
        public DbSet<VisitorStatistic> VisitorStatistics { set; get; }
        public DbSet<Error> Errors { set; get; }
        public DbSet<ApplicationGroup> ApplicationGroups { set; get; }
        public DbSet<ApplicationRole> ApplicationRoles { set; get; }
        public DbSet<ApplicationRoleGroup> ApplicationRoleGroups { set; get; }
        public DbSet<ApplicationUserGroup> ApplicationUserGroups { set; get; }

        public static PharmaDbContext Create()
        {
            return new PharmaDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserRole>().HasKey(i =>new { i.UserId, i.RoleId }).ToTable("ApplicationUserRoles");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("ApplicationUserLogins");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("ApplicationUserClaims");
            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRoles");
        }
    }
}