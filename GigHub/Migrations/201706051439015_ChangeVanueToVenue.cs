namespace GigHub.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeVanueToVenue : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "Venue", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Gigs", "Vanue");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gigs", "Vanue", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Gigs", "Venue");
        }
    }
}
