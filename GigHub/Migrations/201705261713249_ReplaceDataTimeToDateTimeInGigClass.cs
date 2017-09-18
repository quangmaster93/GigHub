namespace GigHub.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ReplaceDataTimeToDateTimeInGigClass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gigs", "DateTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Gigs", "DataTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Gigs", "DataTime", c => c.DateTime(nullable: false));
            DropColumn("dbo.Gigs", "DateTime");
        }
    }
}
