namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sort_ManyToMany_EntityKind : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sort", "Sort_ManyToMany_EntityKind", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sort", "Sort_ManyToMany_EntityKind");
        }
    }
}
