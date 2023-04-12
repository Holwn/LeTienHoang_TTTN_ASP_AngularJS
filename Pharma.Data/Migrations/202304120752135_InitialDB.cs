namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DeliveryNoteItems",
                c => new
                    {
                        NoteID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        BatchPrice = c.Decimal(precision: 18, scale: 2),
                        RetailPrice = c.Decimal(precision: 18, scale: 2),
                        Quantity = c.Int(),
                        VAT = c.Int(),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        BatchNumber = c.String(maxLength: 256),
                        ExpiredDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.NoteID, t.ProductID })
                .ForeignKey("dbo.DeliveryNotes", t => t.NoteID, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.NoteID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.DeliveryNotes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 128),
                        Number = c.String(maxLength: 128),
                        Date = c.DateTime(),
                        CustomerID = c.Int(nullable: false),
                        Description = c.String(maxLength: 256),
                        VAT = c.Int(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        PaymentAmount = c.Decimal(precision: 18, scale: 2),
                        PaymentMethod = c.String(maxLength: 256),
                        StoreID = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .ForeignKey("dbo.Subjects", t => t.CustomerID, cascadeDelete: true)
                .Index(t => t.CustomerID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.Stores",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Description = c.String(maxLength: 500),
                        Barcode = c.String(maxLength: 1024),
                        RefStoreID = c.Int(),
                        OwnerID = c.Int(),
                        RoleID = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.StoreRoles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.StoreRoles",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 128),
                        Description = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Phone = c.String(maxLength: 50),
                        Email = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Description = c.String(maxLength: 500),
                        Barcode = c.String(maxLength: 1024),
                        TypeSub = c.String(maxLength: 50, unicode: false),
                        StoreID = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        CategoryID = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        MoreImage = c.String(storeType: "xml"),
                        BatchPrice = c.Decimal(precision: 18, scale: 2),
                        RetailPrice = c.Decimal(precision: 18, scale: 2),
                        UnitID = c.Int(nullable: false),
                        Barcode = c.String(maxLength: 100),
                        Description = c.String(maxLength: 500),
                        Contents = c.String(maxLength: 2048),
                        Manufacturer = c.String(maxLength: 1024),
                        AddressManufacturer = c.String(maxLength: 1024),
                        StoreID = c.Int(),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryID, cascadeDelete: true)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .ForeignKey("dbo.Units", t => t.UnitID, cascadeDelete: true)
                .Index(t => t.CategoryID)
                .Index(t => t.UnitID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        StoreID = c.Int(),
                        ParentID = c.Int(),
                        Contain = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.Footers",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 50),
                        Contents = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.MenuGroups",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        URL = c.String(nullable: false, maxLength: 256),
                        DisplayOrder = c.Int(),
                        GroupID = c.Int(nullable: false),
                        Target = c.String(maxLength: 10),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.MenuGroups", t => t.GroupID, cascadeDelete: true)
                .Index(t => t.GroupID);
            
            CreateTable(
                "dbo.Pages",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Contents = c.String(),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        Description = c.String(maxLength: 500),
                        ParentID = c.Int(),
                        DisplayOrder = c.Int(),
                        Image = c.String(maxLength: 256),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Alias = c.String(nullable: false, maxLength: 256, unicode: false),
                        CategoryID = c.Int(nullable: false),
                        Image = c.String(maxLength: 256),
                        Description = c.String(maxLength: 500),
                        Contents = c.String(),
                        MetaKeyword = c.String(maxLength: 256),
                        MetaDescription = c.String(maxLength: 256),
                        HomeFlag = c.Boolean(),
                        HotFlag = c.Boolean(),
                        ViewCount = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PostCategories", t => t.CategoryID, cascadeDelete: true)
                .Index(t => t.CategoryID);
            
            CreateTable(
                "dbo.PostTags",
                c => new
                    {
                        PostID = c.Int(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.PostID, t.TagID })
                .ForeignKey("dbo.Posts", t => t.PostID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.PostID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128, unicode: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Type = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProductMappings",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ProductID = c.Int(nullable: false),
                        BatchInPrice = c.Decimal(precision: 18, scale: 2),
                        BatchInDate = c.DateTime(),
                        RetailInPrice = c.Decimal(precision: 18, scale: 2),
                        RetailInDate = c.DateTime(),
                        BatchNumber = c.String(maxLength: 256),
                        ExpiredDate = c.DateTime(),
                        Quantity = c.Int(),
                        UnitID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.UnitID)
                .Index(t => t.ProductID)
                .Index(t => t.UnitID);
            
            CreateTable(
                "dbo.ProductTags",
                c => new
                    {
                        ProductID = c.Int(nullable: false),
                        TagID = c.String(nullable: false, maxLength: 128, unicode: false),
                    })
                .PrimaryKey(t => new { t.ProductID, t.TagID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagID, cascadeDelete: true)
                .Index(t => t.ProductID)
                .Index(t => t.TagID);
            
            CreateTable(
                "dbo.ReceiptNoteItems",
                c => new
                    {
                        NoteID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                        BatchPrice = c.Decimal(precision: 18, scale: 2),
                        RetailPrice = c.Decimal(precision: 18, scale: 2),
                        VAT = c.Int(),
                        Discount = c.Decimal(precision: 18, scale: 2),
                        BatchNumber = c.String(maxLength: 256),
                        ExpiredDate = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.NoteID, t.ProductID })
                .ForeignKey("dbo.Products", t => t.ProductID, cascadeDelete: true)
                .ForeignKey("dbo.ReceiptNotes", t => t.NoteID, cascadeDelete: true)
                .Index(t => t.NoteID)
                .Index(t => t.ProductID);
            
            CreateTable(
                "dbo.ReceiptNotes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 128),
                        Number = c.String(maxLength: 128),
                        Date = c.DateTime(),
                        SupplierID = c.Int(nullable: false),
                        Description = c.String(maxLength: 256),
                        VAT = c.Int(),
                        Amount = c.Decimal(precision: 18, scale: 2),
                        PaymentAmount = c.Decimal(precision: 18, scale: 2),
                        PaymentMethod = c.String(maxLength: 256),
                        StoreID = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.Int(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Stores", t => t.StoreID)
                .ForeignKey("dbo.Subjects", t => t.SupplierID, cascadeDelete: true)
                .Index(t => t.SupplierID)
                .Index(t => t.StoreID);
            
            CreateTable(
                "dbo.Slides",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Description = c.String(maxLength: 256),
                        Image = c.String(maxLength: 256),
                        Url = c.String(maxLength: 256),
                        DisplayOrder = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SupportOnlines",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Department = c.String(maxLength: 256),
                        Skype = c.String(maxLength: 256),
                        Mobile = c.String(maxLength: 50),
                        Email = c.String(maxLength: 256),
                        Facebook = c.String(maxLength: 256),
                        DisplayOrder = c.Int(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.SystemConfigs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false, maxLength: 50, unicode: false),
                        ValueString = c.String(maxLength: 50),
                        ValueInt = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.VisitorStatistics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        VisitedDate = c.DateTime(nullable: false),
                        IPAddress = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ReceiptNoteItems", "NoteID", "dbo.ReceiptNotes");
            DropForeignKey("dbo.ReceiptNotes", "SupplierID", "dbo.Subjects");
            DropForeignKey("dbo.ReceiptNotes", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.ReceiptNoteItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.ProductTags", "ProductID", "dbo.Products");
            DropForeignKey("dbo.ProductMappings", "UnitID", "dbo.Units");
            DropForeignKey("dbo.ProductMappings", "ProductID", "dbo.Products");
            DropForeignKey("dbo.PostTags", "TagID", "dbo.Tags");
            DropForeignKey("dbo.PostTags", "PostID", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryID", "dbo.PostCategories");
            DropForeignKey("dbo.Menus", "GroupID", "dbo.MenuGroups");
            DropForeignKey("dbo.DeliveryNoteItems", "ProductID", "dbo.Products");
            DropForeignKey("dbo.Products", "UnitID", "dbo.Units");
            DropForeignKey("dbo.Units", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.Products", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.Products", "CategoryID", "dbo.ProductCategories");
            DropForeignKey("dbo.DeliveryNoteItems", "NoteID", "dbo.DeliveryNotes");
            DropForeignKey("dbo.DeliveryNotes", "CustomerID", "dbo.Subjects");
            DropForeignKey("dbo.Subjects", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.DeliveryNotes", "StoreID", "dbo.Stores");
            DropForeignKey("dbo.Stores", "RoleID", "dbo.StoreRoles");
            DropIndex("dbo.ReceiptNotes", new[] { "StoreID" });
            DropIndex("dbo.ReceiptNotes", new[] { "SupplierID" });
            DropIndex("dbo.ReceiptNoteItems", new[] { "ProductID" });
            DropIndex("dbo.ReceiptNoteItems", new[] { "NoteID" });
            DropIndex("dbo.ProductTags", new[] { "TagID" });
            DropIndex("dbo.ProductTags", new[] { "ProductID" });
            DropIndex("dbo.ProductMappings", new[] { "UnitID" });
            DropIndex("dbo.ProductMappings", new[] { "ProductID" });
            DropIndex("dbo.PostTags", new[] { "TagID" });
            DropIndex("dbo.PostTags", new[] { "PostID" });
            DropIndex("dbo.Posts", new[] { "CategoryID" });
            DropIndex("dbo.Menus", new[] { "GroupID" });
            DropIndex("dbo.Units", new[] { "StoreID" });
            DropIndex("dbo.Products", new[] { "StoreID" });
            DropIndex("dbo.Products", new[] { "UnitID" });
            DropIndex("dbo.Products", new[] { "CategoryID" });
            DropIndex("dbo.Subjects", new[] { "StoreID" });
            DropIndex("dbo.Stores", new[] { "RoleID" });
            DropIndex("dbo.DeliveryNotes", new[] { "StoreID" });
            DropIndex("dbo.DeliveryNotes", new[] { "CustomerID" });
            DropIndex("dbo.DeliveryNoteItems", new[] { "ProductID" });
            DropIndex("dbo.DeliveryNoteItems", new[] { "NoteID" });
            DropTable("dbo.VisitorStatistics");
            DropTable("dbo.SystemConfigs");
            DropTable("dbo.SupportOnlines");
            DropTable("dbo.Slides");
            DropTable("dbo.ReceiptNotes");
            DropTable("dbo.ReceiptNoteItems");
            DropTable("dbo.ProductTags");
            DropTable("dbo.ProductMappings");
            DropTable("dbo.Tags");
            DropTable("dbo.PostTags");
            DropTable("dbo.Posts");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Pages");
            DropTable("dbo.Menus");
            DropTable("dbo.MenuGroups");
            DropTable("dbo.Footers");
            DropTable("dbo.Units");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.Products");
            DropTable("dbo.Subjects");
            DropTable("dbo.StoreRoles");
            DropTable("dbo.Stores");
            DropTable("dbo.DeliveryNotes");
            DropTable("dbo.DeliveryNoteItems");
        }
    }
}
