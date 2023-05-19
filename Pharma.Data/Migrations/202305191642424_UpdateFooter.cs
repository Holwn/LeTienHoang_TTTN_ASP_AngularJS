namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFooter : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Footers");
            AddColumn("dbo.Footers", "Name", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Footers", "Type", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Footers", "Link", c => c.String(maxLength: 256));
            AlterColumn("dbo.Footers", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Footers", "ID");
            DropColumn("dbo.Footers", "Contents");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Footers", "Contents", c => c.String(nullable: false));
            DropPrimaryKey("dbo.Footers");
            AlterColumn("dbo.Footers", "ID", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Footers", "Link");
            DropColumn("dbo.Footers", "Type");
            DropColumn("dbo.Footers", "Name");
            AddPrimaryKey("dbo.Footers", "ID");
        }
    }
}
