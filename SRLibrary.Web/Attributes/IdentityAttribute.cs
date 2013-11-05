using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftRight.Web.Attributes {

	/// <summary>
	/// Equivalent to [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	/// </summary>
	public class IdentityAttribute: DatabaseGeneratedAttribute {
		public IdentityAttribute(): base(DatabaseGeneratedOption.Identity) {
		}
	}
}
