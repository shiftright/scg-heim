namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Added_Variant_FilePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FloorVariants", "PlanPreviewImageFilePath", c => c.String());
            AddColumn("dbo.FloorVariants", "ModelFilePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FloorVariants", "ModelFilePath");
            DropColumn("dbo.FloorVariants", "PlanPreviewImageFilePath");
        }
    }
}
