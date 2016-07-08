namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingGridster : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Gridster",
                c => new
                    {
                        GridsterKey = c.Int(nullable: false, identity: true),
                        Gridster_Entity_ID = c.Int(nullable: false),
                        Gridster_Entity_Kind = c.String(nullable: false, maxLength: 50),
                        Gridster_User_ID = c.Int(nullable: false),
                        Gridster_Edited_On = c.DateTime(),
                        Gridster_ManyToMany_ID = c.Int(),
                        cols = c.Int(nullable: false),
                        rows = c.Int(nullable: false),
                        y = c.Int(nullable: false),
                        x = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GridsterKey)
                .ForeignKey("dbo.User", t => t.Gridster_User_ID, cascadeDelete: true)
                .Index(t => t.Gridster_User_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gridster", "Gridster_User_ID", "dbo.User");
            DropIndex("dbo.Gridster", new[] { "Gridster_User_ID" });
            DropTable("dbo.Gridster");
        }
    }
}
