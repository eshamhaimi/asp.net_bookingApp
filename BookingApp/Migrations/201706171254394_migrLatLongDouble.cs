namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrLatLongDouble : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Accommodations", "Latitude", c => c.Double(nullable: false));
            AlterColumn("dbo.Accommodations", "Longitude", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Accommodations", "Longitude", c => c.String());
            AlterColumn("dbo.Accommodations", "Latitude", c => c.String());
        }
    }
}
