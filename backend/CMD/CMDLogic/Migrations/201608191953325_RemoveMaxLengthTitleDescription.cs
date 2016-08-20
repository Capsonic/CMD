namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveMaxLengthTitleDescription : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Metric", "Title", c => c.String());
            AlterColumn("dbo.Metric", "Description", c => c.String());
            AlterColumn("dbo.Initiative", "Title", c => c.String());
            AlterColumn("dbo.Initiative", "Description", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Initiative", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Initiative", "Title", c => c.String(maxLength: 100));
            AlterColumn("dbo.Metric", "Description", c => c.String(maxLength: 250));
            AlterColumn("dbo.Metric", "Title", c => c.String(maxLength: 100));
        }
    }
}
