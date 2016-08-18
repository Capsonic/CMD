namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTitleToMetricAndInitiative : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Metric", "Title", c => c.String(maxLength: 100));
            AddColumn("dbo.Initiative", "Title", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Initiative", "Title");
            DropColumn("dbo.Metric", "Title");
        }
    }
}
