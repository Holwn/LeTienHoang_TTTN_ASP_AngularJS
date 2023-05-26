namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReceiptAndDeliveyNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DeliveryNotes", "DeliveryNoteProductItems", c => c.String());
            AddColumn("dbo.ReceiptNotes", "ReceiptNoteProductItems", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceiptNotes", "ReceiptNoteProductItems");
            DropColumn("dbo.DeliveryNotes", "DeliveryNoteProductItems");
        }
    }
}
