using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using ShiftRight.Web.Attributes;

namespace ShiftRight.Heim.Models {

	public interface IPreviewable {
		Uri GetPreview();
	}

	#region Models

	public class Area {
		public float Usage { get; set; }
		public float Land { get; set; }
	}
	
	public class Attribute {

		[Key, Identity]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }
		[Required]
		public string Value { get; set; }
		[Required]
		public string Unit { get; set; }
	}

	//public class Floor : IPreviewable {
	//	public int ID { get; set; }
	//	public string Name { get; set; }
	//	public int FloorIndex { get; set; }
	//}

	public class Floor: IPreviewable {

		[Key, Identity]
		public int ID { get; set; }

		[Required]
		public int FloorNumber { get; set; }

		[Required]
		public string Name { get; set; }

		public byte[] Data { get; set; }

		public Floor() {
			Data = new byte[] { };
		}
		
		public Uri GetPreview() {
			throw new NotImplementedException();
		}
	}

	public class Plan : IPreviewable {

		[Key, Identity]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }
		public Area Area { get; set; }
		public HashSet<Floor> Floors { get; set; }

		public byte[] Data { get; set; }
		public bool IsTemplate { get; set; }

		public Plan() {
			Data = new byte[] { };
		}

		public Uri GetPreview() {
			throw new NotImplementedException();
		}
	}
	
	public class Project: IPreviewable{

		[Key, Identity]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }

		[Required]
		public int OwnerID { get; set; }
		
		public Plan Plan { get; set; }

		[ForeignKey("OwnerID")]
		public UserProfile Owner { get; set; }
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset Updated { get; set; }

		public bool IsDeleted { get; set; }

		public Uri GetPreview() {
			if(Plan != null) {
				return Plan.GetPreview();
			}

			return null;
		}
	}

	#endregion

	#region ViewModels

	public class ProjectHomeViewModel {
		public IEnumerable<ProjectViewModel> Projects { get; set; }
	}

	public class NewProjectViewModel {
		public string Search { get; set; }
		public IEnumerable<PlanViewModel> Plans { get; set; }
	}

	public class CustomizeProjectViewModel {
		public int ID { get; set; }
		public string Name { get; set; }
		public PlanViewModel Plan { get; set; }

		public string PreviewImage { get; set; }

		public IEnumerable<Attribute> Attributes { get; set; }
	}

	public class ProjectViewModel {
		public int ID { get; set; }
		public string Name { get; set; }
		public string PreviewImage { get; set; }

		public DateTimeOffset Created { get; set; }
		public PlanViewModel Plan { get; set; }

		public UserProfile Owner { get; set; }
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
		public Area Area { get; set; }
	}

	#endregion

}