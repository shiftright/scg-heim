using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using ShiftRight.Web.Attributes;

namespace ShiftRight.Heim.Models {


	public class Plan : IPreviewable {

		[Key, Identity]
		public int ID { get; set; }

		[Required]
		public string Name { get; set; }
		public Area Area { get; set; }
		public int Price { get; set; }
		public HashSet<FloorTemplate> Floors { get; set; }
		public HashSet<Attribute> Attributes { get; set; }

		public string Data { get; set; }
		public bool IsTemplate { get; set; }

		[DataType("DATETIMEOFFSET(2)")]
		public DateTimeOffset Updated { get; set; }

		[DataType("DATETIMEOFFSET(2)")]
		public DateTimeOffset Created { get; set; }
		public string PreviewImageFilePath { get; set; }

		public string ModelFilePath { get; set; }
		
		public Plan() {
			//Data = new byte[] { };
			IsTemplate = false;
		}

		public Uri GetPreview() {
			return new Uri("/Resources/Plans/Preview/1_" + Updated.UtcTicks + ".jpg", UriKind.Relative);
		}

	}
}