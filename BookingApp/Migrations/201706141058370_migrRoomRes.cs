namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrRoomRes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RoomReservations", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.RoomReservations", new[] { "Room_Id" });
            AlterColumn("dbo.RoomReservations", "Room_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.RoomReservations", "Room_Id");
            AddForeignKey("dbo.RoomReservations", "Room_Id", "dbo.Rooms", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RoomReservations", "Room_Id", "dbo.Rooms");
            DropIndex("dbo.RoomReservations", new[] { "Room_Id" });
            AlterColumn("dbo.RoomReservations", "Room_Id", c => c.Int());
            CreateIndex("dbo.RoomReservations", "Room_Id");
            AddForeignKey("dbo.RoomReservations", "Room_Id", "dbo.Rooms", "Id");
        }
    }
}
