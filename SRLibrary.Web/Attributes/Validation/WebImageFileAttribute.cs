using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ShiftRight.Web.Attributes.Validation {

	public class WebImageFileAttribute : ValidationAttribute {

		public static readonly string[] WebImages = {
			".jpe", ".jpeg", ".jpg", ".png", ".gif"
		};

		public WebImageFileAttribute() {
			AllowedExtensions = WebImages;
		}

		/// <summary>
		/// Example: .jpg, .png, .gif
		/// </summary>
		public string[] AllowedExtensions { get; set; }

		public override bool IsValid(object value) {
			var file = value as HttpPostedFileBase;

			if(file != null) {

				if(AllowedExtensions == null) {
					return true;
				} else if(AllowedExtensions.Contains("*")) {
					return true;
				} else {
					string ext = file.FileName.Substring(file.FileName.LastIndexOf('.'));
					ext = ext.ToLower();

					if(AllowedExtensions.Contains(ext)) {
						return true;
					} else {
						ErrorMessage = String.Format("Files with extension '{0}' is not allowed", ext);
						return false;
					}
				}

			} else {
				return true;
			}
		}
	}
}
