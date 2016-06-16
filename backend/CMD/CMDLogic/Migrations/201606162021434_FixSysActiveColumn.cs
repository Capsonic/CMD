namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixSysActiveColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Objective", "sys_active", c => c.Boolean(nullable: false));
            DropColumn("dbo.Objective", "sys_status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Objective", "sys_status", c => c.Boolean(nullable: false));
            DropColumn("dbo.Objective", "sys_active");
        }
    }
}
