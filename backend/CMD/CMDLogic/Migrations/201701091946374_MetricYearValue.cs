namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MetricYearValue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MetricYear", "Value", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.MetricYear", "Value");
        }
    }
}
