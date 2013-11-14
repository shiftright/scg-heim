namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Removed_FloorName : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Floors", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Floors", "Name", c => c.String(nullable: false));
        }
    }
}
