namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrRoomAccFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Rooms", "Accommodation_Id", "dbo.Accommodations");
            DropIndex("dbo.Rooms", new[] { "Accommodation_Id" });
            RenameColumn(table: "dbo.Rooms", name: "Accommodation_Id", newName: "Acc_Id");
            AlterColumn("dbo.Rooms", "Acc_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Rooms", "Acc_Id");
            AddForeignKey("dbo.Rooms", "Acc_Id", "dbo.Accommodations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Rooms", "Acc_Id", "dbo.Accommodations");
            DropIndex("dbo.Rooms", new[] { "Acc_Id" });
            AlterColumn("dbo.Rooms", "Acc_Id", c => c.Int());
            RenameColumn(table: "dbo.Rooms", name: "Acc_Id", newName: "Accommodation_Id");
            CreateIndex("dbo.Rooms", "Accommodation_Id");
            AddForeignKey("dbo.Rooms", "Accommodation_Id", "dbo.Accommodations", "Id");
        }
    }
}
