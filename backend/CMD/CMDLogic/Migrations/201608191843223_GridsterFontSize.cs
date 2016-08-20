namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GridsterFontSize : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gridster", "FontSize", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gridster", "FontSize");
        }
    }
}
