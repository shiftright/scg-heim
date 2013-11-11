namespace ShiftRight.Heim.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Plans",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Area_Usage = c.Single(nullable: false),
                        Area_Land = c.Single(nullable: false),
                        Data = c.Binary(),
                        IsTemplate = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Floors",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FloorNumber = c.Int(nullable: false),
                        Name = c.String(nullable: false),
                        Data = c.Binary(),
                        Plan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Plans", t => t.Plan_ID)
                .Index(t => t.Plan_ID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        OwnerID = c.Int(nullable: false),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Updated = c.DateTimeOffset(nullable: false, precision: 7),
                        IsDeleted = c.Boolean(nullable: false),
                        Plan_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.OwnerID, cascadeDelete: true)
                .ForeignKey("dbo.Plans", t => t.Plan_ID)
                .Index(t => t.OwnerID)
                .Index(t => t.Plan_ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "Plan_ID", "dbo.Plans");
            DropForeignKey("dbo.Projects", "OwnerID", "dbo.Users");
            DropForeignKey("dbo.Floors", "Plan_ID", "dbo.Plans");
            DropIndex("dbo.Projects", new[] { "Plan_ID" });
            DropIndex("dbo.Projects", new[] { "OwnerID" });
            DropIndex("dbo.Floors", new[] { "Plan_ID" });
            DropTable("dbo.Users");
            DropTable("dbo.Projects");
            DropTable("dbo.Floors");
            DropTable("dbo.Plans");
        }
    }
}
