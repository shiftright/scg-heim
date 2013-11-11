namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Email : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Username", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Username", c => c.String());
            DropColumn("dbo.Users", "Email");
        }
    }
}
