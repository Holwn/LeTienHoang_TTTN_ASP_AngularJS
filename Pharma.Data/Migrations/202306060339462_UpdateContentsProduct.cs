namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateContentsProduct : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Products", "Contents", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Contents", c => c.String(maxLength: 2048));
        }
    }
}
