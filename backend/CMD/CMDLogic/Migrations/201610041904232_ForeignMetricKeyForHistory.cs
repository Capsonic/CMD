namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ForeignMetricKeyForHistory : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MetricHistory", "Metric_MetricKey", "dbo.Metric");
            DropIndex("dbo.MetricHistory", new[] { "Metric_MetricKey" });
            RenameColumn(table: "dbo.MetricHistory", name: "Metric_MetricKey", newName: "MetricKey");
            AlterColumn("dbo.MetricHistory", "MetricKey", c => c.Int(nullable: false));
            CreateIndex("dbo.MetricHistory", "MetricKey");
            AddForeignKey("dbo.MetricHistory", "MetricKey", "dbo.Metric", "MetricKey", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MetricHistory", "MetricKey", "dbo.Metric");
            DropIndex("dbo.MetricHistory", new[] { "MetricKey" });
            AlterColumn("dbo.MetricHistory", "MetricKey", c => c.Int());
            RenameColumn(table: "dbo.MetricHistory", name: "MetricKey", newName: "Metric_MetricKey");
            CreateIndex("dbo.MetricHistory", "Metric_MetricKey");
            AddForeignKey("dbo.MetricHistory", "Metric_MetricKey", "dbo.Metric", "MetricKey");
        }
    }
}
