namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Plan_Preview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plans", "Updated", c => c.DateTimeOffset(nullable: false, precision: 7));
            AddColumn("dbo.Plans", "Created", c => c.DateTimeOffset(nullable: false, precision: 7));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plans", "Created");
            DropColumn("dbo.Plans", "Updated");
        }
    }
}
