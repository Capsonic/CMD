namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMetricYear : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MetricHistory", "MetricKey", "dbo.Metric");
            DropIndex("dbo.MetricHistory", new[] { "MetricKey" });
            CreateTable(
                "dbo.MetricYear",
                c => new
                    {
                        MetricYearKey = c.Int(nullable: false, identity: true),
                        Note = c.String(),
                        MetricKey = c.Int(nullable: false),
                        sys_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MetricYearKey)
                .ForeignKey("dbo.Metric", t => t.MetricKey, cascadeDelete: true)
                .Index(t => t.MetricKey);
            
            AddColumn("dbo.MetricHistory", "MetricYearKey", c => c.Int(nullable: false));
            CreateIndex("dbo.MetricHistory", "MetricYearKey");
            AddForeignKey("dbo.MetricHistory", "MetricYearKey", "dbo.MetricYear", "MetricYearKey", cascadeDelete: true);
            DropColumn("dbo.MetricHistory", "MetricKey");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MetricHistory", "MetricKey", c => c.Int(nullable: false));
            DropForeignKey("dbo.MetricHistory", "MetricYearKey", "dbo.MetricYear");
            DropForeignKey("dbo.MetricYear", "MetricKey", "dbo.Metric");
            DropIndex("dbo.MetricHistory", new[] { "MetricYearKey" });
            DropIndex("dbo.MetricYear", new[] { "MetricKey" });
            DropColumn("dbo.MetricHistory", "MetricYearKey");
            DropTable("dbo.MetricYear");
            CreateIndex("dbo.MetricHistory", "MetricKey");
            AddForeignKey("dbo.MetricHistory", "MetricKey", "dbo.Metric", "MetricKey", cascadeDelete: true);
        }
    }
}
