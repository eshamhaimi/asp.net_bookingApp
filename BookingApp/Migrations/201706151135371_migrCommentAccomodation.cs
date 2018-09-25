namespace BookingApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migrCommentAccomodation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Accommodation_Id", "dbo.Accommodations");
            DropIndex("dbo.Comments", new[] { "Accommodation_Id" });
            RenameColumn(table: "dbo.Comments", name: "Accommodation_Id", newName: "Acc_Id");
            AlterColumn("dbo.Comments", "Acc_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.Comments", "Acc_Id");
            AddForeignKey("dbo.Comments", "Acc_Id", "dbo.Accommodations", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Acc_Id", "dbo.Accommodations");
            DropIndex("dbo.Comments", new[] { "Acc_Id" });
            AlterColumn("dbo.Comments", "Acc_Id", c => c.Int());
            RenameColumn(table: "dbo.Comments", name: "Acc_Id", newName: "Accommodation_Id");
            CreateIndex("dbo.Comments", "Accommodation_Id");
            AddForeignKey("dbo.Comments", "Accommodation_Id", "dbo.Accommodations", "Id");
        }
    }
}
