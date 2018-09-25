namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrAccomodations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Accommodations", "AccommodationType_Id", "dbo.AccommodationTypes");
            DropIndex("dbo.Accommodations", new[] { "AccommodationType_Id" });
            AlterColumn("dbo.Accommodations", "AccommodationType_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Accommodations", "AccommodationType_Id");
            AddForeignKey("dbo.Accommodations", "AccommodationType_Id", "dbo.AccommodationTypes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Accommodations", "AccommodationType_Id", "dbo.AccommodationTypes");
            DropIndex("dbo.Accommodations", new[] { "AccommodationType_Id" });
            AlterColumn("dbo.Accommodations", "AccommodationType_Id", c => c.Int());
            CreateIndex("dbo.Accommodations", "AccommodationType_Id");
            AddForeignKey("dbo.Accommodations", "AccommodationType_Id", "dbo.AccommodationTypes", "Id");
        }
    }
}
