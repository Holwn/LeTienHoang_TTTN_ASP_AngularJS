namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsHaveExDInProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "IsHaveExpiredDate", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "IsHaveExpiredDate");
        }
    }
}
