namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrAccommodationsPlace : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accommodations", "Place_Id", "dbo.Places");
            DropIndex("dbo.Accommodations", new[] { "Place_Id" });
            AlterColumn("dbo.Accommodations", "Place_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Accommodations", "Place_Id");
            AddForeignKey("dbo.Accommodations", "Place_Id", "dbo.Places", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accommodations", "Place_Id", "dbo.Places");
            DropIndex("dbo.Accommodations", new[] { "Place_Id" });
            AlterColumn("dbo.Accommodations", "Place_Id", c => c.Int());
            CreateIndex("dbo.Accommodations", "Place_Id");
            AddForeignKey("dbo.Accommodations", "Place_Id", "dbo.Places", "Id");
        }
    }
}
