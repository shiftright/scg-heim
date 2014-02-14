namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPriceToPlan : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plans", "Price", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Plans", "Price");
        }
    }
}
