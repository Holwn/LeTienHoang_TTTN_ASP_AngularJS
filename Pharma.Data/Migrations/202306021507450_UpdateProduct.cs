﻿namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Prescription", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Prescription");
        }
    }
}
