using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShiftRight.Heim.Models {

	public class HeimDbContext: DbContext {

		public HeimDbContext()
			: base("DefaultConnection") {
		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<Plan> Plans { get; set; }
		public DbSet<UserProfile> UserProfiles { get; set; }
	}
}