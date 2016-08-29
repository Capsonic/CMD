namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GridsterFontSizeNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Gridster", "FontSize", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Gridster", "FontSize", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
