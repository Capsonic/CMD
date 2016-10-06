namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dashboardshared : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gridster", "IsShared", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gridster", "IsShared");
        }
    }
}
