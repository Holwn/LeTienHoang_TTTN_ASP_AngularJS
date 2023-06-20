namespace Pharma.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameTableIdentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdentityRoles", newName: "ApplicationRoles");
            RenameTable(name: "dbo.IdentityUserRoles", newName: "ApplicationUserRoles");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "ApplicationUserClaims");
            RenameTable(name: "dbo.IdentityUserLogins", newName: "ApplicationUserLogins");
            DropPrimaryKey("dbo.ApplicationUserClaims");
            AlterColumn("dbo.ApplicationUserClaims", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.ApplicationUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ApplicationUserClaims", "UserId");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserClaims");
            AlterColumn("dbo.ApplicationUserClaims", "UserId", c => c.String());
            AlterColumn("dbo.ApplicationUserClaims", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ApplicationUserClaims", "Id");
            RenameTable(name: "dbo.ApplicationUserLogins", newName: "IdentityUserLogins");
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "IdentityUserClaims");
            RenameTable(name: "dbo.ApplicationUserRoles", newName: "IdentityUserRoles");
            RenameTable(name: "dbo.ApplicationRoles", newName: "IdentityRoles");
        }
    }
}
