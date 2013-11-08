using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ShiftRight.Heim.Models {

	#region ViewModels

	public class ProjectHomeViewModel {
		public IEnumerable<Project> Projects { get; set; }
	}

	public class NewProjectViewModel {
		public string Search { get; set; }
		public IEnumerable<HousePlan> Plans { get; set; }
	}

	public class CustomizeProjectViewModel {
		public int ID { get; set; }
		public string Name { get; set; }
		public PlanViewModel Plan { get; set; }

		public string PreviewImage { get; set; }

		public IEnumerable<Attribute> Attributes { get; set; }
	}

	public class FloorViewModel {

		public int ID { get; set; }
		public int FloorNumber { get; set; }
		public string Name { get; set; }

		public IList<FloorViewModel> Variants { get; set; }

		public string PlanPreviewImage { get; set; }

		public bool IsDefault { get; set; }
	}

	public class PlanViewModel {
		public int ID { get; set; }
		public string Name { get; set; }
		public string PreviewImage { get; set; }

		public IList<FloorViewModel> Floors { get; set; }
	}

	#endregion

	#region DataContex

	public class ProjectContext : DbContext {

		public ProjectContext()
			: base("DefaultConnection") {
		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<HousePlan> Plans { get; set; }
	}

	#endregion

	public class Area {
		public float Usage { get; set; }
		public float Land { get; set; }
	}

	public class HousePlan {
		public int ID { get; set; }
		public string Name { get; set; }
		public string PreviewImage { get; set; }
		public Area Area { get; set; }
	}

	public class Attribute {
		public int ID { get; set; }
		public string Name { get; set; }
		public string Value { get; set; }
		public string Unit { get; set; }
	}

	public abstract class Floor {
		public int ID { get; set; }
		public int FloorIndex { get; set; }
		public string Name { get; set; }

		public IList<FloorVariant> Variants { get; set; }
	}

	public class FloorVariant: Floor {
	}

	public class Project{
		public int ID { get; set; }
		public string Name { get; set; }
		public string PreviewImage { get; set; }
		public HousePlan Plan { get; set; }

		public UserProfile Owner { get; set; }
		public DateTimeOffset Created { get; set; }
	}
}