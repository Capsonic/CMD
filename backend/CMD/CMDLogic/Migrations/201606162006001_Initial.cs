namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.cat_ComparatorMethod",
                c => new
                    {
                        ComparatorMethodKey = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ComparatorMethodKey);
            
            CreateTable(
                "dbo.Metric",
                c => new
                    {
                        MetricKey = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 250),
                        CurrentValue = c.Decimal(precision: 15, scale: 4),
                        GoalValue = c.Decimal(precision: 15, scale: 4),
                        FormatKey = c.Int(),
                        BasisKey = c.Int(),
                        ComparatorMethodKey = c.Int(),
                        sys_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.MetricKey)
                .ForeignKey("dbo.cat_ComparatorMethod", t => t.ComparatorMethodKey)
                .ForeignKey("dbo.cat_MetricBasis", t => t.BasisKey)
                .ForeignKey("dbo.cat_MetricFormat", t => t.FormatKey)
                .Index(t => t.FormatKey)
                .Index(t => t.BasisKey)
                .Index(t => t.ComparatorMethodKey);
            
            CreateTable(
                "dbo.cat_MetricBasis",
                c => new
                    {
                        MetricBasisKey = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MetricBasisKey);
            
            CreateTable(
                "dbo.cat_MetricFormat",
                c => new
                    {
                        MetricFormatKey = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.MetricFormatKey);
            
            CreateTable(
                "dbo.Objective",
                c => new
                    {
                        ObjectiveKey = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 250),
                        sys_status = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ObjectiveKey);
            
            CreateTable(
                "dbo.Dashboard",
                c => new
                    {
                        DashboardKey = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 150),
                        Description = c.String(maxLength: 300),
                        sys_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.DashboardKey);
            
            CreateTable(
                "dbo.Initiative",
                c => new
                    {
                        InitiativeKey = c.Int(nullable: false, identity: true),
                        ProgressValue = c.Decimal(nullable: false, precision: 15, scale: 4),
                        Description = c.String(maxLength: 250),
                        DueDate = c.DateTime(),
                        ActualDate = c.DateTime(),
                        sys_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.InitiativeKey);
            
            CreateTable(
                "dbo.Gant",
                c => new
                    {
                        GantKey = c.Int(nullable: false, identity: true),
                        InitiativeKey = c.Int(nullable: false),
                        GantData = c.String(unicode: false),
                        sys_active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.GantKey)
                .ForeignKey("dbo.Initiative", t => t.InitiativeKey)
                .Index(t => t.InitiativeKey);
            
            CreateTable(
                "dbo.Sort",
                c => new
                    {
                        SortKey = c.Int(nullable: false, identity: true),
                        Sort_Entity_ID = c.Int(nullable: false),
                        Sort_Entity_Kind = c.String(nullable: false, maxLength: 50),
                        Sort_User_ID = c.Int(nullable: false),
                        Sort_Edited_On = c.DateTime(),
                        Sort_Sequence = c.Int(),
                        Sort_ManyToMany_ID = c.Int(),
                    })
                .PrimaryKey(t => t.SortKey)
                .ForeignKey("dbo.User", t => t.Sort_User_ID, cascadeDelete: true)
                .Index(t => t.Sort_User_ID);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserKey = c.Int(nullable: false, identity: true),
                        Value = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 20),
                        Role = c.String(maxLength: 50),
                        Email = c.String(maxLength: 256),
                        Phone1 = c.String(maxLength: 50),
                        Phone2 = c.String(maxLength: 50),
                        sys_active = c.Boolean(nullable: false),
                        Identicon = c.Binary(),
                        Identicon64 = c.String(unicode: false),
                    })
                .PrimaryKey(t => t.UserKey);
            
            CreateTable(
                "dbo.Track",
                c => new
                    {
                        TrackKey = c.Int(nullable: false, identity: true),
                        Entity_ID = c.Int(nullable: false),
                        Entity_Kind = c.String(nullable: false, maxLength: 50),
                        User_CreatedBy = c.Int(nullable: false),
                        Date_CreatedOn = c.DateTime(nullable: false),
                        Date_EditedOn = c.DateTime(),
                        Date_RemovedOn = c.DateTime(),
                        Date_LastTimeUsed = c.DateTime(),
                        User_LastEditedBy = c.Int(),
                        User_RemovedBy = c.Int(),
                        User_AssignedTo = c.Int(),
                        User_AssignedBy = c.Int(),
                    })
                .PrimaryKey(t => t.TrackKey)
                .ForeignKey("dbo.User", t => t.User_LastEditedBy)
                .ForeignKey("dbo.User", t => t.User_RemovedBy)
                .ForeignKey("dbo.User", t => t.User_AssignedTo)
                .ForeignKey("dbo.User", t => t.User_AssignedBy)
                .Index(t => t.User_LastEditedBy)
                .Index(t => t.User_RemovedBy)
                .Index(t => t.User_AssignedTo)
                .Index(t => t.User_AssignedBy);
            
            CreateTable(
                "dbo.cross_Dashboard_Objective",
                c => new
                    {
                        DashboardKey = c.Int(nullable: false),
                        ObjectiveKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.DashboardKey, t.ObjectiveKey })
                .ForeignKey("dbo.Dashboard", t => t.DashboardKey, cascadeDelete: true)
                .ForeignKey("dbo.Objective", t => t.ObjectiveKey, cascadeDelete: true)
                .Index(t => t.DashboardKey)
                .Index(t => t.ObjectiveKey);
            
            CreateTable(
                "dbo.cross_Objective_Initiative",
                c => new
                    {
                        InitiativeKey = c.Int(nullable: false),
                        ObjectiveKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.InitiativeKey, t.ObjectiveKey })
                .ForeignKey("dbo.Initiative", t => t.InitiativeKey, cascadeDelete: true)
                .ForeignKey("dbo.Objective", t => t.ObjectiveKey, cascadeDelete: true)
                .Index(t => t.InitiativeKey)
                .Index(t => t.ObjectiveKey);
            
            CreateTable(
                "dbo.cross_Objective_Metric",
                c => new
                    {
                        MetricKey = c.Int(nullable: false),
                        ObjectiveKey = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.MetricKey, t.ObjectiveKey })
                .ForeignKey("dbo.Metric", t => t.MetricKey, cascadeDelete: true)
                .ForeignKey("dbo.Objective", t => t.ObjectiveKey, cascadeDelete: true)
                .Index(t => t.MetricKey)
                .Index(t => t.ObjectiveKey);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Track", "User_AssignedBy", "dbo.User");
            DropForeignKey("dbo.Track", "User_AssignedTo", "dbo.User");
            DropForeignKey("dbo.Track", "User_RemovedBy", "dbo.User");
            DropForeignKey("dbo.Track", "User_LastEditedBy", "dbo.User");
            DropForeignKey("dbo.Sort", "Sort_User_ID", "dbo.User");
            DropForeignKey("dbo.cross_Objective_Metric", "ObjectiveKey", "dbo.Objective");
            DropForeignKey("dbo.cross_Objective_Metric", "MetricKey", "dbo.Metric");
            DropForeignKey("dbo.cross_Objective_Initiative", "ObjectiveKey", "dbo.Objective");
            DropForeignKey("dbo.cross_Objective_Initiative", "InitiativeKey", "dbo.Initiative");
            DropForeignKey("dbo.Gant", "InitiativeKey", "dbo.Initiative");
            DropForeignKey("dbo.cross_Dashboard_Objective", "ObjectiveKey", "dbo.Objective");
            DropForeignKey("dbo.cross_Dashboard_Objective", "DashboardKey", "dbo.Dashboard");
            DropForeignKey("dbo.Metric", "FormatKey", "dbo.cat_MetricFormat");
            DropForeignKey("dbo.Metric", "BasisKey", "dbo.cat_MetricBasis");
            DropForeignKey("dbo.Metric", "ComparatorMethodKey", "dbo.cat_ComparatorMethod");
            DropIndex("dbo.cross_Objective_Metric", new[] { "ObjectiveKey" });
            DropIndex("dbo.cross_Objective_Metric", new[] { "MetricKey" });
            DropIndex("dbo.cross_Objective_Initiative", new[] { "ObjectiveKey" });
            DropIndex("dbo.cross_Objective_Initiative", new[] { "InitiativeKey" });
            DropIndex("dbo.cross_Dashboard_Objective", new[] { "ObjectiveKey" });
            DropIndex("dbo.cross_Dashboard_Objective", new[] { "DashboardKey" });
            DropIndex("dbo.Track", new[] { "User_AssignedBy" });
            DropIndex("dbo.Track", new[] { "User_AssignedTo" });
            DropIndex("dbo.Track", new[] { "User_RemovedBy" });
            DropIndex("dbo.Track", new[] { "User_LastEditedBy" });
            DropIndex("dbo.Sort", new[] { "Sort_User_ID" });
            DropIndex("dbo.Gant", new[] { "InitiativeKey" });
            DropIndex("dbo.Metric", new[] { "ComparatorMethodKey" });
            DropIndex("dbo.Metric", new[] { "BasisKey" });
            DropIndex("dbo.Metric", new[] { "FormatKey" });
            DropTable("dbo.cross_Objective_Metric");
            DropTable("dbo.cross_Objective_Initiative");
            DropTable("dbo.cross_Dashboard_Objective");
            DropTable("dbo.Track");
            DropTable("dbo.User");
            DropTable("dbo.Sort");
            DropTable("dbo.Gant");
            DropTable("dbo.Initiative");
            DropTable("dbo.Dashboard");
            DropTable("dbo.Objective");
            DropTable("dbo.cat_MetricFormat");
            DropTable("dbo.cat_MetricBasis");
            DropTable("dbo.Metric");
            DropTable("dbo.cat_ComparatorMethod");
        }
    }
}
