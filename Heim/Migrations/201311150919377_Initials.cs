namespace ShiftRight.Heim.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class Initials : DbMigration {
		public override void Up() {
			CreateTable(
				"dbo.Floors",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					PlanID = c.Int(nullable: false),
					FloorNumber = c.Int(nullable: false),
					Data = c.String(),
				})
				.PrimaryKey(t => t.ID)
				.ForeignKey("dbo.Plans", t => t.PlanID, cascadeDelete: true)
				.Index(t => t.PlanID);

			CreateTable(
				"dbo.Plans",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					Name = c.String(nullable: false),
					Area_Usage = c.Single(nullable: false),
					Area_Land = c.Single(nullable: false),
					Data = c.String(),
					IsTemplate = c.Boolean(nullable: false),
					Updated = c.DateTimeOffset(nullable: false, precision: 7),
					Created = c.DateTimeOffset(nullable: false, precision: 7),
					PreviewImageFilePath = c.String(),
				})
				.PrimaryKey(t => t.ID);

			CreateTable(
				"dbo.Attributes",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					PlanID = c.Int(nullable: false),
					Name = c.String(nullable: false),
					Value = c.String(nullable: false),
					Unit = c.String(nullable: false),
				})
				.PrimaryKey(t => t.ID)
				.ForeignKey("dbo.Plans", t => t.PlanID, cascadeDelete: true)
				.Index(t => t.PlanID);

			CreateTable(
				"dbo.FloorVariants",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					Name = c.String(nullable: false),
					FloorID = c.Int(nullable: false),
					PlanPreviewImageFilePath = c.String(),
					ModelFilePath = c.String(),
					Created = c.DateTimeOffset(nullable: false, precision: 7),
					Updated = c.DateTimeOffset(nullable: false, precision: 7),
				})
				.PrimaryKey(t => t.ID)
				.ForeignKey("dbo.Floors", t => t.FloorID, cascadeDelete: true)
				.Index(t => t.FloorID);

			CreateTable(
				"dbo.Projects",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					Name = c.String(nullable: false),
					OwnerID = c.Int(nullable: false),
					Data = c.String(),
					IsDeleted = c.Boolean(nullable: false),
					PlanTemplateID = c.Int(nullable: false),
					Created = c.DateTimeOffset(nullable: false, precision: 7),
					Updated = c.DateTimeOffset(nullable: false, precision: 7),
				})
				.PrimaryKey(t => t.ID)
				.ForeignKey("dbo.Users", t => t.OwnerID, cascadeDelete: true)
				.ForeignKey("dbo.Plans", t => t.PlanTemplateID, cascadeDelete: true)
				.Index(t => t.OwnerID)
				.Index(t => t.PlanTemplateID);

			CreateTable(
				"dbo.Users",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					Username = c.String(nullable: false),
					Email = c.String(nullable: false),
				})
				.PrimaryKey(t => t.ID);

		}

		public override void Down() {
			DropForeignKey("dbo.Projects", "PlanTemplateID", "dbo.Plans");
			DropForeignKey("dbo.Projects", "OwnerID", "dbo.Users");
			DropForeignKey("dbo.FloorVariants", "FloorID", "dbo.Floors");
			DropForeignKey("dbo.Floors", "PlanID", "dbo.Plans");
			DropForeignKey("dbo.Attributes", "PlanID", "dbo.Plans");
			DropIndex("dbo.Projects", new[] { "PlanTemplateID" });
			DropIndex("dbo.Projects", new[] { "OwnerID" });
			DropIndex("dbo.FloorVariants", new[] { "FloorID" });
			DropIndex("dbo.Floors", new[] { "PlanID" });
			DropIndex("dbo.Attributes", new[] { "PlanID" });
			DropTable("dbo.Users");
			DropTable("dbo.Projects");
			DropTable("dbo.FloorVariants");
			DropTable("dbo.Attributes");
			DropTable("dbo.Plans");
			DropTable("dbo.Floors");
		}
	}
}