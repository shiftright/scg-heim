namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_PlanModelFilePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plans", "ModelFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plans", "ModelFilePath");
        }
    }
}
