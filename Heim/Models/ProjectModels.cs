﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using ShiftRight.Web.Attributes;
using ShiftRight.Web.Attributes.Validation;

namespace ShiftRight.Heim.Models {

	public interface IPreviewable {
		Uri GetPreview();
	}

	#region Models

	public class Area {

		[DisplayName("Usage area (sqm.)")]
		[Required(ErrorMessage = "Usage area is required")]
		public float Usage { get; set; }

		[DisplayName("Land area (sqm.)")]
		[Required(ErrorMessage = "Land area is required")]
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

	public class FloorVariant{

		[Key, Identity]
		public int ID { get; set; }

		//[Required]
		//public int FloorNumber { get; set; }

		[Required]
		public string Name { get; set; }

		public int FloorID { get; set; }

		public string PlanPreviewImageFilePath { get; set; }

		public string ModelFilePath { get; set; }

		[DataType("DATETIMEOFFSET(2)")]
		public DateTimeOffset Created { get; set; }

		[DataType("DATETIMEOFFSET(2)")]
		public DateTimeOffset Updated { get; set; }

		[ForeignKey("FloorID")]
		public Floor Floor { get; set; }

	}

	public class Floor{

		[Key]
		public int ID { get; set; }

		[Required]
		public int PlanID { get; set; }

		[Required]
		public int FloorNumber { get; set; }

		//[Required]
		//public string Name { get; set; }

		public byte[] Data { get; set; }

		[ForeignKey("PlanID")]
		public Plan Plan { get; set; }

		public HashSet<FloorVariant> Variants { get; set; }

		public Floor() {
			Data = new byte[] { };
			Variants = new HashSet<FloorVariant>();
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

	public class FloorVariantViewModel {
		public int ID { get; set; }
		public int FloorID { get; set; }
		public int FloorNumber { get; set; }
		public int PlanID { get; set; }

		[Required]
		public string Name { get; set; }
		public bool IsDefault { get; set; }
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset Updated { get; set; }

		[DisplayName("Model file")]
		public HttpPostedFileBase ModelFile { get; set; }

		[DisplayName("Plan image file")]
		public HttpPostedFileBase PlanImageFile { get; set; }

		public string ModelFilePath { get; set; }
		public string PlanPreviewImageFilePath { get; set; }

		public double ModelFileSize { get; set; }
	}

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

		[DisplayName("Floor")]
		public int FloorNumber { get; set; }

		public IEnumerable<FloorVariantViewModel> Variants { get; set; }

		public string PlanPreviewImage { get; set; }
		public PlanViewModel Plan { get; set; }

		public bool IsDefault { get; set; }

		public FloorViewModel() {
			Variants = new List<FloorVariantViewModel>();
		}

		public string Name {
			get {
				return "FL" + FloorNumber;
			}
		}
	}

	public class PlanViewModel {
		public int ID { get; set; }

		public string PreviewImage { get; set; }

		[Required]
		public string Name { get; set; }

		[WebImageFile]
		[DisplayName("Preview image file")]
		public HttpPostedFileBase PreviewImageFile { get; set; }

		public Area Area { get; set; }

		[DisplayName("Floors")]
		public IEnumerable<FloorViewModel> Floors { get; set; }
	}

	#endregion

}