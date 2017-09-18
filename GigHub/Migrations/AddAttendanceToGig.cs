namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttendanceToGig : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Attendances", "GigId");
            AddForeignKey("dbo.Attendances", "GigId", "dbo.Gigs", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attendances", "GigId", "dbo.Gigs");
            DropIndex("dbo.Attendances", new[] { "GigId" });
        }
    }
}
