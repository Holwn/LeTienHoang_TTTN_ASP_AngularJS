namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDeilveryNoteItemPrice : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryNoteItems", "Price", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.DeliveryNoteItems", "BatchPrice");
            DropColumn("dbo.DeliveryNoteItems", "RetailPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DeliveryNoteItems", "RetailPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.DeliveryNoteItems", "BatchPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.DeliveryNoteItems", "Price");
        }
    }
}
