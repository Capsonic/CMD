namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gridsterOptionalUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Gridster", "Gridster_User_ID", "dbo.User");
            DropIndex("dbo.Gridster", new[] { "Gridster_User_ID" });
            AlterColumn("dbo.Gridster", "Gridster_User_ID", c => c.Int());
            CreateIndex("dbo.Gridster", "Gridster_User_ID");
            AddForeignKey("dbo.Gridster", "Gridster_User_ID", "dbo.User", "UserKey");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gridster", "Gridster_User_ID", "dbo.User");
            DropIndex("dbo.Gridster", new[] { "Gridster_User_ID" });
            AlterColumn("dbo.Gridster", "Gridster_User_ID", c => c.Int(nullable: false));
            CreateIndex("dbo.Gridster", "Gridster_User_ID");
            AddForeignKey("dbo.Gridster", "Gridster_User_ID", "dbo.User", "UserKey", cascadeDelete: true);
        }
    }
}
