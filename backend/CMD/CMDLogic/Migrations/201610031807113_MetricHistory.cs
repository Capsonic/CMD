namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetricHistory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MetricHistory",
                c => new
                    {
                        MetricHistoryKey = c.Int(nullable: false, identity: true),
                        Note = c.String(),
                        CurrentValue = c.Decimal(precision: 15, scale: 4),
                        GoalValue = c.Decimal(precision: 15, scale: 4),
                        Metric_MetricKey = c.Int(),
                    })
                .PrimaryKey(t => t.MetricHistoryKey)
                .ForeignKey("dbo.Metric", t => t.Metric_MetricKey)
                .Index(t => t.Metric_MetricKey);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MetricHistory", "Metric_MetricKey", "dbo.Metric");
            DropIndex("dbo.MetricHistory", new[] { "Metric_MetricKey" });
            DropTable("dbo.MetricHistory");
        }
    }
}
