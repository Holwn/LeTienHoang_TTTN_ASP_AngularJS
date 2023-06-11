namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProductMapping : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ProductMappings");
            AddPrimaryKey("dbo.ProductMappings", new[] { "ID", "ProductID" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ProductMappings");
            AddPrimaryKey("dbo.ProductMappings", "ID");
        }
    }
}
