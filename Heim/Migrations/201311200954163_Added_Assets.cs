namespace ShiftRight.Heim.Migrations {
	using System;
	using System.Data.Entity.Migrations;

	public partial class Added_Assets : DbMigration {
		public override void Up() {
			CreateTable(
				"dbo.Assets",
				c => new {
					ID = c.Int(nullable: false, identity: true),
					Name = c.String(nullable: false),
					Mapping = c.String(),
					AssetFilePath = c.String(),
					PreviewFilePath = c.String(),
					AssetType = c.Int(nullable: false),
					Created = c.DateTimeOffset(nullable: false, precision: 7),
					Updated = c.DateTimeOffset(nullable: false, precision: 7),
					Discriminator = c.String(nullable: false, maxLength: 128),
				})
				.PrimaryKey(t => t.ID);

		}

		public override void Down() {
			DropTable("dbo.Assets");
		}
	}
}