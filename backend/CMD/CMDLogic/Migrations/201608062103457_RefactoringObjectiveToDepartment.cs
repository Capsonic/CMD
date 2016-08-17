namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RefactoringObjectiveToDepartment : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.cross_Dashboard_Objective", "DashboardKey", "dbo.Dashboard");
            DropForeignKey("dbo.cross_Dashboard_Objective", "ObjectiveKey", "dbo.Objective");
            DropForeignKey("dbo.cross_Objective_Initiative", "InitiativeKey", "dbo.Initiative");
            DropForeignKey("dbo.cross_Objective_Initiative", "ObjectiveKey", "dbo.Objective");
            DropForeignKey("dbo.cross_Objective_Metric", "MetricKey", "dbo.Metric");
            DropForeignKey("dbo.cross_Objective_Metric", "ObjectiveKey", "dbo.Objective");
            DropIndex("dbo.cross_Dashboard_Objective", new[] { "DashboardKey" });
            DropIndex("dbo.cross_Dashboard_Objective", new[] { "ObjectiveKey" });
            DropIndex("dbo.cross_Objective_Initiative", new[] { "InitiativeKey" });
            DropIndex("dbo.cross_Objective_Initiative", new[] { "ObjectiveKey" });
            DropIndex("dbo.cross_Objective_Metric", new[] { "MetricKey" });
            DropIndex("dbo.cross_Objective_Metric", new[] { "ObjectiveKey" });
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentKey = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        sys_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DepartmentKey);
            
            CreateTable(
                "dbo.cross_Dashboard_Department",
                c => new
                    {
                        DashboardKey = c.Int(nullable: false),
                        DepartmentKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DashboardKey, t.DepartmentKey })
                .ForeignKey("dbo.Dashboard", t => t.DashboardKey, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.DepartmentKey, cascadeDelete: true)
                .Index(t => t.DashboardKey)
                .Index(t => t.DepartmentKey);
            
            CreateTable(
                "dbo.cross_Department_Initiative",
                c => new
                    {
                        InitiativeKey = c.Int(nullable: false),
                        DepartmentKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InitiativeKey, t.DepartmentKey })
                .ForeignKey("dbo.Initiative", t => t.InitiativeKey, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.DepartmentKey, cascadeDelete: true)
                .Index(t => t.InitiativeKey)
                .Index(t => t.DepartmentKey);
            
            CreateTable(
                "dbo.cross_Department_Metric",
                c => new
                    {
                        MetricKey = c.Int(nullable: false),
                        DepartmentKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MetricKey, t.DepartmentKey })
                .ForeignKey("dbo.Metric", t => t.MetricKey, cascadeDelete: true)
                .ForeignKey("dbo.Department", t => t.DepartmentKey, cascadeDelete: true)
                .Index(t => t.MetricKey)
                .Index(t => t.DepartmentKey);
            
            DropTable("dbo.Objective");
            DropTable("dbo.cross_Dashboard_Objective");
            DropTable("dbo.cross_Objective_Initiative");
            DropTable("dbo.cross_Objective_Metric");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.cross_Objective_Metric",
                c => new
                    {
                        MetricKey = c.Int(nullable: false),
                        ObjectiveKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MetricKey, t.ObjectiveKey });
            
            CreateTable(
                "dbo.cross_Objective_Initiative",
                c => new
                    {
                        InitiativeKey = c.Int(nullable: false),
                        ObjectiveKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InitiativeKey, t.ObjectiveKey });
            
            CreateTable(
                "dbo.cross_Dashboard_Objective",
                c => new
                    {
                        DashboardKey = c.Int(nullable: false),
                        ObjectiveKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DashboardKey, t.ObjectiveKey });
            
            CreateTable(
                "dbo.Objective",
                c => new
                    {
                        ObjectiveKey = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        sys_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ObjectiveKey);
            
            DropForeignKey("dbo.cross_Department_Metric", "DepartmentKey", "dbo.Department");
            DropForeignKey("dbo.cross_Department_Metric", "MetricKey", "dbo.Metric");
            DropForeignKey("dbo.cross_Department_Initiative", "DepartmentKey", "dbo.Department");
            DropForeignKey("dbo.cross_Department_Initiative", "InitiativeKey", "dbo.Initiative");
            DropForeignKey("dbo.cross_Dashboard_Department", "DepartmentKey", "dbo.Department");
            DropForeignKey("dbo.cross_Dashboard_Department", "DashboardKey", "dbo.Dashboard");
            DropIndex("dbo.cross_Department_Metric", new[] { "DepartmentKey" });
            DropIndex("dbo.cross_Department_Metric", new[] { "MetricKey" });
            DropIndex("dbo.cross_Department_Initiative", new[] { "DepartmentKey" });
            DropIndex("dbo.cross_Department_Initiative", new[] { "InitiativeKey" });
            DropIndex("dbo.cross_Dashboard_Department", new[] { "DepartmentKey" });
            DropIndex("dbo.cross_Dashboard_Department", new[] { "DashboardKey" });
            DropTable("dbo.cross_Department_Metric");
            DropTable("dbo.cross_Department_Initiative");
            DropTable("dbo.cross_Dashboard_Department");
            DropTable("dbo.Department");
            CreateIndex("dbo.cross_Objective_Metric", "ObjectiveKey");
            CreateIndex("dbo.cross_Objective_Metric", "MetricKey");
            CreateIndex("dbo.cross_Objective_Initiative", "ObjectiveKey");
            CreateIndex("dbo.cross_Objective_Initiative", "InitiativeKey");
            CreateIndex("dbo.cross_Dashboard_Objective", "ObjectiveKey");
            CreateIndex("dbo.cross_Dashboard_Objective", "DashboardKey");
            AddForeignKey("dbo.cross_Objective_Metric", "ObjectiveKey", "dbo.Objective", "ObjectiveKey", cascadeDelete: true);
            AddForeignKey("dbo.cross_Objective_Metric", "MetricKey", "dbo.Metric", "MetricKey", cascadeDelete: true);
            AddForeignKey("dbo.cross_Objective_Initiative", "ObjectiveKey", "dbo.Objective", "ObjectiveKey", cascadeDelete: true);
            AddForeignKey("dbo.cross_Objective_Initiative", "InitiativeKey", "dbo.Initiative", "InitiativeKey", cascadeDelete: true);
            AddForeignKey("dbo.cross_Dashboard_Objective", "ObjectiveKey", "dbo.Objective", "ObjectiveKey", cascadeDelete: true);
            AddForeignKey("dbo.cross_Dashboard_Objective", "DashboardKey", "dbo.Dashboard", "DashboardKey", cascadeDelete: true);
        }
    }
}
