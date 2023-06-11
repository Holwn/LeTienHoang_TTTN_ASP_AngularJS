namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateFooters : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Footers", "ParentID", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Footers", "ParentID");
        }
    }
}
