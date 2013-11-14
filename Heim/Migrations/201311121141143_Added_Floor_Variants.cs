namespace ShiftRight.Heim.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class Added_Floor_Variants : DbMigration {
		public override void Up() {
			DropForeignKey("dbo.Floors", "Plan_ID", "dbo.Plans");
			DropIndex("dbo.Floors", new[] { "Plan_ID" });
			RenameColumn(table: "dbo.Floors", name: "Plan_ID", newName: "PlanID");
			CreateTable(
				"dbo.FloorVariants",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					FloorNumber = c.Int(nullable: false),
					FloorID = c.Int(nullable: false),
					Created = c.DateTimeOffset(nullable: false, precision: 7),
					Updated = c.DateTimeOffset(nullable: false, precision: 7),
				})
				.PrimaryKey(t => t.ID)
				.ForeignKey("dbo.Floors", t => t.FloorID, cascadeDelete: true)
				.Index(t => t.FloorID);

			AlterColumn("dbo.Floors", "PlanID", c => c.Int(nullable: false));
			CreateIndex("dbo.Floors", "PlanID");
			AddForeignKey("dbo.Floors", "PlanID", "dbo.Plans", "ID", cascadeDelete: true);
		}

		public override void Down() {
			DropForeignKey("dbo.Floors", "PlanID", "dbo.Plans");
			DropForeignKey("dbo.FloorVariants", "FloorID", "dbo.Floors");
			DropIndex("dbo.Floors", new[] { "PlanID" });
			DropIndex("dbo.FloorVariants", new[] { "FloorID" });
			AlterColumn("dbo.Floors", "PlanID", c => c.Int());
			DropTable("dbo.FloorVariants");
			RenameColumn(table: "dbo.Floors", name: "PlanID", newName: "Plan_ID");
			CreateIndex("dbo.Floors", "Plan_ID");
			AddForeignKey("dbo.Floors", "Plan_ID", "dbo.Plans", "ID");
		}
	}
}