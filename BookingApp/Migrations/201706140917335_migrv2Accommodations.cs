namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrv2Accommodations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accommodations", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.RoomReservations", "User_Id", "dbo.Users");
            DropIndex("dbo.Accommodations", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.RoomReservations", new[] { "User_Id" });
            AddColumn("dbo.Accommodations", "AppUser_Id", c => c.Int(nullable: false));
            AddColumn("dbo.Comments", "AppUser_Id", c => c.Int());
            AddColumn("dbo.RoomReservations", "AppUser_Id", c => c.Int());
            CreateIndex("dbo.Accommodations", "AppUser_Id");
            CreateIndex("dbo.Comments", "AppUser_Id");
            CreateIndex("dbo.RoomReservations", "AppUser_Id");
            AddForeignKey("dbo.Accommodations", "AppUser_Id", "dbo.AppUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "AppUser_Id", "dbo.AppUsers", "Id");
            AddForeignKey("dbo.RoomReservations", "AppUser_Id", "dbo.AppUsers", "Id");
            DropColumn("dbo.Accommodations", "User_Id");
            DropColumn("dbo.Comments", "User_Id");
            DropColumn("dbo.RoomReservations", "User_Id");
            DropTable("dbo.Users");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.RoomReservations", "User_Id", c => c.Int());
            AddColumn("dbo.Comments", "User_Id", c => c.Int());
            AddColumn("dbo.Accommodations", "User_Id", c => c.Int());
            DropForeignKey("dbo.RoomReservations", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Comments", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.Accommodations", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.RoomReservations", new[] { "AppUser_Id" });
            DropIndex("dbo.Comments", new[] { "AppUser_Id" });
            DropIndex("dbo.Accommodations", new[] { "AppUser_Id" });
            DropColumn("dbo.RoomReservations", "AppUser_Id");
            DropColumn("dbo.Comments", "AppUser_Id");
            DropColumn("dbo.Accommodations", "AppUser_Id");
            CreateIndex("dbo.RoomReservations", "User_Id");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Accommodations", "User_Id");
            AddForeignKey("dbo.RoomReservations", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Accommodations", "User_Id", "dbo.Users", "Id");
        }
    }
}
