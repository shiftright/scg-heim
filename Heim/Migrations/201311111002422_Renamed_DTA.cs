namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamed_DTA : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "Username", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
        }
    }
}
