namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateSomeTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Products", "UnitID", "dbo.Units");
            DropForeignKey("dbo.ProductMappings", "UnitID", "dbo.Units");
            DropIndex("dbo.Products", new[] { "UnitID" });
            DropIndex("dbo.ProductMappings", new[] { "UnitID" });
            AddColumn("dbo.Products", "BatchUnitID", c => c.Int());
            AddColumn("dbo.Products", "RetailUnitID", c => c.Int(nullable: false));
            AddColumn("dbo.Products", "Contain", c => c.Int());
            AddColumn("dbo.Products", "MiduleUnitID", c => c.Int());
            AddColumn("dbo.Products", "ContainMidule", c => c.Int());
            AddColumn("dbo.Products", "Quantity", c => c.Int());
            AddColumn("dbo.Products", "BatchNumber", c => c.String(maxLength: 256));
            AddColumn("dbo.Products", "ExpiredDate", c => c.DateTime());
            AddColumn("dbo.Units", "Description", c => c.String(maxLength: 500));
            AddColumn("dbo.ProductMappings", "RetailID", c => c.Int());
            AddColumn("dbo.ProductMappings", "BatchNumber", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductMappings", "ExpiredDate", c => c.DateTime());
            AddColumn("dbo.ReceiptNoteItems", "Price", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Products", "UnitID");
            DropColumn("dbo.Units", "ParentID");
            DropColumn("dbo.Units", "Contain");
            DropColumn("dbo.ProductMappings", "UnitID");
            DropColumn("dbo.ReceiptNoteItems", "BatchPrice");
            DropColumn("dbo.ReceiptNoteItems", "RetailPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReceiptNoteItems", "RetailPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ReceiptNoteItems", "BatchPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductMappings", "UnitID", c => c.Int());
            AddColumn("dbo.Units", "Contain", c => c.Int());
            AddColumn("dbo.Units", "ParentID", c => c.Int());
            AddColumn("dbo.Products", "UnitID", c => c.Int(nullable: false));
            DropColumn("dbo.ReceiptNoteItems", "Price");
            DropColumn("dbo.ProductMappings", "ExpiredDate");
            DropColumn("dbo.ProductMappings", "BatchNumber");
            DropColumn("dbo.ProductMappings", "RetailID");
            DropColumn("dbo.Units", "Description");
            DropColumn("dbo.Products", "ExpiredDate");
            DropColumn("dbo.Products", "BatchNumber");
            DropColumn("dbo.Products", "Quantity");
            DropColumn("dbo.Products", "ContainMidule");
            DropColumn("dbo.Products", "MiduleUnitID");
            DropColumn("dbo.Products", "Contain");
            DropColumn("dbo.Products", "RetailUnitID");
            DropColumn("dbo.Products", "BatchUnitID");
            CreateIndex("dbo.ProductMappings", "UnitID");
            CreateIndex("dbo.Products", "UnitID");
            AddForeignKey("dbo.ProductMappings", "UnitID", "dbo.Units", "ID");
            AddForeignKey("dbo.Products", "UnitID", "dbo.Units", "ID", cascadeDelete: true);
        }
    }
}
