using Pharma.Model.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pharma.Data
{
    public class PharmaDbContext : DbContext
    {
        public PharmaDbContext() : base("PharmaConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<DeliveryNote> DeliveryNotes { set; get; }
        public DbSet<DeliveryNoteItem> DeliveryNoteItems { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Menu> Menus { set; get; }
        public DbSet<MenuGroup> MenuGroups { set; get; }
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

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}