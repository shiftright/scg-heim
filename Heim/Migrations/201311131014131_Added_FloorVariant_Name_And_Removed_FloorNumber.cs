namespace ShiftRight.Heim.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class Added_FloorVariant_Name_And_Removed_FloorNumber : DbMigration {
		public override void Up() {
			AddColumn("dbo.FloorVariants", "Name", c => c.String(nullable: false));
			DropColumn("dbo.FloorVariants", "FloorNumber");
		}

		public override void Down() {
			AddColumn("dbo.FloorVariants", "FloorNumber", c => c.Int(nullable: false));
			DropColumn("dbo.FloorVariants", "Name");
		}
	}
}