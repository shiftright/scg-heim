using ShiftRight.Web.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShiftRight.Heim.Models {

	public enum AssetType {
		Tile = 1,
		RoofTile = 2,
		Furniture = 3,
		Wallpaper = 4,
		Electricity = 5
	}

	public class Asset {

		[Key, Identity]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }

		public string Mapping { get; set; }
		
		public string AssetFilePath { get; set; }
		public string PreviewFilePath { get; set; }

		[DisplayName("Asset type")]
		public AssetType AssetType { get; set; }
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset Updated { get; set; }
	}
}