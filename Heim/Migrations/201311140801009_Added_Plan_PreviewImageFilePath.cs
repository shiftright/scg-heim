namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Plan_PreviewImageFilePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plans", "PreviewImageFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plans", "PreviewImageFilePath");
        }
    }
}
