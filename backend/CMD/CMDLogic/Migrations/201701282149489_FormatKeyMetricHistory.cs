namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FormatKeyMetricHistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetricHistory", "FormatKey", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetricHistory", "FormatKey");
        }
    }
}
