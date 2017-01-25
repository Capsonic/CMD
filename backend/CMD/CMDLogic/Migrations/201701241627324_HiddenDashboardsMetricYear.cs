namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class HiddenDashboardsMetricYear : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetricYear", "HiddenForDashboards", c => c.String());
            DropColumn("dbo.Metric", "HiddenForDashboards");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Metric", "HiddenForDashboards", c => c.String());
            DropColumn("dbo.MetricYear", "HiddenForDashboards");
        }
    }
}
