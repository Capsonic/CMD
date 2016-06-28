namespace CMDLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationTrackCreatedBy : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Track", name: "User_LastEditedBy", newName: "User_LastEditedByKey");
            RenameColumn(table: "dbo.Track", name: "User_RemovedBy", newName: "User_RemovedByKey");
            RenameColumn(table: "dbo.Track", name: "User_AssignedTo", newName: "User_AssignedToKey");
            RenameColumn(table: "dbo.Track", name: "User_AssignedBy", newName: "User_AssignedByKey");
            RenameIndex(table: "dbo.Track", name: "IX_User_LastEditedBy", newName: "IX_User_LastEditedByKey");
            RenameIndex(table: "dbo.Track", name: "IX_User_RemovedBy", newName: "IX_User_RemovedByKey");
            RenameIndex(table: "dbo.Track", name: "IX_User_AssignedTo", newName: "IX_User_AssignedToKey");
            RenameIndex(table: "dbo.Track", name: "IX_User_AssignedBy", newName: "IX_User_AssignedByKey");
            AddColumn("dbo.Track", "User_CreatedByKey", c => c.Int(nullable: false));
            CreateIndex("dbo.Track", "User_CreatedByKey");
            AddForeignKey("dbo.Track", "User_CreatedByKey", "dbo.User", "UserKey", cascadeDelete: true);
            DropColumn("dbo.Track", "User_CreatedBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Track", "User_CreatedBy", c => c.Int(nullable: false));
            DropForeignKey("dbo.Track", "User_CreatedByKey", "dbo.User");
            DropIndex("dbo.Track", new[] { "User_CreatedByKey" });
            DropColumn("dbo.Track", "User_CreatedByKey");
            RenameIndex(table: "dbo.Track", name: "IX_User_AssignedByKey", newName: "IX_User_AssignedBy");
            RenameIndex(table: "dbo.Track", name: "IX_User_AssignedToKey", newName: "IX_User_AssignedTo");
            RenameIndex(table: "dbo.Track", name: "IX_User_RemovedByKey", newName: "IX_User_RemovedBy");
            RenameIndex(table: "dbo.Track", name: "IX_User_LastEditedByKey", newName: "IX_User_LastEditedBy");
            RenameColumn(table: "dbo.Track", name: "User_AssignedByKey", newName: "User_AssignedBy");
            RenameColumn(table: "dbo.Track", name: "User_AssignedToKey", newName: "User_AssignedTo");
            RenameColumn(table: "dbo.Track", name: "User_RemovedByKey", newName: "User_RemovedBy");
            RenameColumn(table: "dbo.Track", name: "User_LastEditedByKey", newName: "User_LastEditedBy");
        }
    }
}
