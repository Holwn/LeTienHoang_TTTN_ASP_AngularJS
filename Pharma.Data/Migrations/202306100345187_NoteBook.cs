namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NoteBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NoteBooks",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        Contents = c.String(),
                        ModelTargetLink = c.String(maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(),
                        Status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.NoteBooks");
        }
    }
}
