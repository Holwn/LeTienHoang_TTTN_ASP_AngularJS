namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateReceiptNoteItem : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReceiptNoteItems", "Quantity", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReceiptNoteItems", "Quantity");
        }
    }
}
