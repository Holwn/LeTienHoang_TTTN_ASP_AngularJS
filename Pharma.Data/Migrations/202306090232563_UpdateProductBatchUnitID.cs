namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductBatchUnitID : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "BatchUnitID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "BatchUnitID", c => c.Int());
        }
    }
}
