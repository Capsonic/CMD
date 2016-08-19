namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetricsInitiativesHiddenDashboards : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Metric", "HiddenForDashboards", c => c.String());
            AddColumn("dbo.Initiative", "HiddenForDashboards", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Initiative", "HiddenForDashboards");
            DropColumn("dbo.Metric", "HiddenForDashboards");
        }
    }
}
