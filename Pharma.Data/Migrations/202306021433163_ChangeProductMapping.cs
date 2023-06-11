namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeProductMapping : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductMappings", "ReceiptQuantity", c => c.Int());
            AddColumn("dbo.ProductMappings", "ReceiptPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.ProductMappings", "DeliveryQuantity", c => c.Int());
            AddColumn("dbo.ProductMappings", "DeliveryPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.ProductMappings", "BatchInPrice");
            DropColumn("dbo.ProductMappings", "BatchInDate");
            DropColumn("dbo.ProductMappings", "RetailInPrice");
            DropColumn("dbo.ProductMappings", "RetailInDate");
            DropColumn("dbo.ProductMappings", "BatchNumber");
            DropColumn("dbo.ProductMappings", "ExpiredDate");
            DropColumn("dbo.ProductMappings", "Quantity");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ProductMappings", "Quantity", c => c.Int());
            AddColumn("dbo.ProductMappings", "ExpiredDate", c => c.DateTime());
            AddColumn("dbo.ProductMappings", "BatchNumber", c => c.String(maxLength: 256));
            AddColumn("dbo.ProductMappings", "RetailInDate", c => c.DateTime());
            AddColumn("dbo.ProductMappings", "RetailInPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.ProductMappings", "BatchInDate", c => c.DateTime());
            AddColumn("dbo.ProductMappings", "BatchInPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.ProductMappings", "DeliveryPrice");
            DropColumn("dbo.ProductMappings", "DeliveryQuantity");
            DropColumn("dbo.ProductMappings", "ReceiptPrice");
            DropColumn("dbo.ProductMappings", "ReceiptQuantity");
        }
    }
}
