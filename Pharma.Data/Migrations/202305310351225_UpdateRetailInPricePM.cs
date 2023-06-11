namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateRetailInPricePM : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductMappings", "RetailInPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductMappings", "RetailInPrice", c => c.Decimal(precision: 18, scale: 2));
        }
    }
}
