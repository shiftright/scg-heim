using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShiftRight.Heim.Models
{
    public class HeimContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<ShiftRight.Heim.Models.HeimContext>());

		public HeimContext()
			: base("DefaultConnection") {
		}

		public DbSet<ShiftRight.Heim.Models.FloorTemplate> Floors { get; set; }
		public DbSet<FloorVariant> FloorVariants { get; set; }
		public DbSet<Project> Projects { get; set; }
		public DbSet<Plan> Plans { get; set; }
		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<Attribute> Attributes { get; set; }
		public DbSet<Asset> Assets { get; set; }
	}
}