namespace VolunteerTracker.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedRegexPhone : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Volunteers", "HomeNumber", c => c.String());
            AlterColumn("dbo.Volunteers", "CellNumber", c => c.String());
            AlterColumn("dbo.Volunteers", "WorkNumber", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Volunteers", "WorkNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Volunteers", "CellNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Volunteers", "HomeNumber", c => c.Int(nullable: false));
        }
    }
}
