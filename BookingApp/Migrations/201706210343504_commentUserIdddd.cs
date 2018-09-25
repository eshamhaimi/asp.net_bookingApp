namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class commentUserIdddd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Comments", name: "AppUser_Id", newName: "User_Id");
            RenameIndex(table: "dbo.Comments", name: "IX_AppUser_Id", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_User_Id", newName: "IX_AppUser_Id");
            RenameColumn(table: "dbo.Comments", name: "User_Id", newName: "AppUser_Id");
        }
    }
}
