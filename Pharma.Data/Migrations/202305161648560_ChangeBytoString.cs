namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeBytoString : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.DeliveryNotes", "CreatedBy", c => c.String());
            AlterColumn("dbo.DeliveryNotes", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Stores", "CreatedBy", c => c.String());
            AlterColumn("dbo.Stores", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Subjects", "CreatedBy", c => c.String());
            AlterColumn("dbo.Subjects", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Products", "CreatedBy", c => c.String());
            AlterColumn("dbo.Products", "UpdatedBy", c => c.String());
            AlterColumn("dbo.ProductCategories", "CreatedBy", c => c.String());
            AlterColumn("dbo.ProductCategories", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Pages", "CreatedBy", c => c.String());
            AlterColumn("dbo.Pages", "UpdatedBy", c => c.String());
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.String());
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.String());
            AlterColumn("dbo.Posts", "CreatedBy", c => c.String());
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.String());
            AlterColumn("dbo.ReceiptNotes", "CreatedBy", c => c.String());
            AlterColumn("dbo.ReceiptNotes", "UpdatedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReceiptNotes", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.ReceiptNotes", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Posts", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Posts", "CreatedBy", c => c.Int());
            AlterColumn("dbo.PostCategories", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.PostCategories", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Pages", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Pages", "CreatedBy", c => c.Int());
            AlterColumn("dbo.ProductCategories", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.ProductCategories", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Products", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Products", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Subjects", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Subjects", "CreatedBy", c => c.Int());
            AlterColumn("dbo.Stores", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.Stores", "CreatedBy", c => c.Int());
            AlterColumn("dbo.DeliveryNotes", "UpdatedBy", c => c.Int());
            AlterColumn("dbo.DeliveryNotes", "CreatedBy", c => c.Int());
        }
    }
}
