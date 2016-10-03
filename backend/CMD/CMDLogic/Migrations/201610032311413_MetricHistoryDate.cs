namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetricHistoryDate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetricHistory", "MetricDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetricHistory", "MetricDate");
        }
    }
}
