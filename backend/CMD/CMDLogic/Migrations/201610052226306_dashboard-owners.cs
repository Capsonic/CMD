namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dashboardowners : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Dashboard", "Owners", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Dashboard", "Owners");
        }
    }
}
