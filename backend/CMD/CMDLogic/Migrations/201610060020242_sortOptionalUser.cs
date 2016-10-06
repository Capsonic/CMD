namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sortOptionalUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sort", "Sort_User_ID", "dbo.User");
            DropIndex("dbo.Sort", new[] { "Sort_User_ID" });
            AddColumn("dbo.Sort", "IsShared", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Sort", "Sort_User_ID", c => c.Int());
            CreateIndex("dbo.Sort", "Sort_User_ID");
            AddForeignKey("dbo.Sort", "Sort_User_ID", "dbo.User", "UserKey");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sort", "Sort_User_ID", "dbo.User");
            DropIndex("dbo.Sort", new[] { "Sort_User_ID" });
            AlterColumn("dbo.Sort", "Sort_User_ID", c => c.Int(nullable: false));
            DropColumn("dbo.Sort", "IsShared");
            CreateIndex("dbo.Sort", "Sort_User_ID");
            AddForeignKey("dbo.Sort", "Sort_User_ID", "dbo.User", "UserKey", cascadeDelete: true);
        }
    }
}
