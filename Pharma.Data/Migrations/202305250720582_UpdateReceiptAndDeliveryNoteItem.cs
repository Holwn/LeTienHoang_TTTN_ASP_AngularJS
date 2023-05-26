namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReceiptAndDeliveryNoteItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryNoteItems", "UnitID", c => c.Int());
            AddColumn("dbo.ReceiptNoteItems", "UnitID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceiptNoteItems", "UnitID");
            DropColumn("dbo.DeliveryNoteItems", "UnitID");
        }
    }
}
