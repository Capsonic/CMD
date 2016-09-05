namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sort_parentInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Sort", "Sort_ParentInfo", c => c.String());
            DropColumn("dbo.Sort", "Sort_ManyToMany_ID");
            DropColumn("dbo.Sort", "Sort_ManyToMany_EntityKind");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sort", "Sort_ManyToMany_EntityKind", c => c.String());
            AddColumn("dbo.Sort", "Sort_ManyToMany_ID", c => c.Int());
            DropColumn("dbo.Sort", "Sort_ParentInfo");
        }
    }
}
