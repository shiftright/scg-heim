using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShiftRight.Heim.Models {

	public class ProjectViewModel {
		public IEnumerable<Project> Projects { get; set; }
	}

	public class Area {
		public float Usage { get; set; }
		public float Land { get; set; }
	}

	public class HousePlan {
		public int ID { get; set; }
		public string Name { get; set; }
		public Area Area { get; set; }
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