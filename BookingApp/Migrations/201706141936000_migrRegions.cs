namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrRegions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Regions", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Regions", new[] { "Country_Id" });
            AlterColumn("dbo.Regions", "Country_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Regions", "Country_Id");
            AddForeignKey("dbo.Regions", "Country_Id", "dbo.Countries", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Regions", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Regions", new[] { "Country_Id" });
            AlterColumn("dbo.Regions", "Country_Id", c => c.Int());
            CreateIndex("dbo.Regions", "Country_Id");
            AddForeignKey("dbo.Regions", "Country_Id", "dbo.Countries", "Id");
        }
    }
}
