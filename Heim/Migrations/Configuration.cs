namespace ShiftRight.Heim.Migrations {
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;
	using System.Web.Security;
	using WebMatrix.WebData;

	internal sealed class Configuration : DbMigrationsConfiguration<ShiftRight.Heim.Models.HeimContext> {
		public Configuration() {
			AutomaticMigrationsEnabled = false;
		}

		protected override void Seed(ShiftRight.Heim.Models.HeimContext context) {
			//  This method will be called after migrating to the latest version.

			//  You can use the DbSet<T>.AddOrUpdate() helper extension method 
			//  to avoid creating duplicate seed data. E.g.
			//
			//    context.People.AddOrUpdate(
			//      p => p.FullName,
			//      new Person { FullName = "Andrew Peters" },
			//      new Person { FullName = "Brice Lambson" },
			//      new Person { FullName = "Rowan Miller" }
			//    );
			//

			WebSecurity.InitializeDatabaseConnection("DefaultConnection", "Users", "ID", "UserName", autoCreateTables: true);

			if (!Roles.RoleExists("admin")) {
				Roles.CreateRole("admin");
			}
			if (!WebSecurity.UserExists("admin")) {
				WebSecurity.CreateUserAndAccount("admin", "heim1234", new {
					Email = "pisin.r@gmail.com"
				});
			}
			if(!Roles.GetRolesForUser("admin").Contains("admin")) {
				Roles.AddUserToRole("admin", "admin");
			}


		}
	}
}