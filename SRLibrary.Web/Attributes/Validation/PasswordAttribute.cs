using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftRight.Web.Attributes.Validation {

	/// <summary>
	/// Equivalent to [DataType(DataType.Password)]
	/// </summary>
	public class PasswordAttribute: DataTypeAttribute {
		public PasswordAttribute(): base(DataType.Password) {

		}
	}
}
