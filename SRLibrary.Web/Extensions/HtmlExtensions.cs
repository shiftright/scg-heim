using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace System.Web.Mvc {
	public static class HtmlExtensions {
		
		/// <summary>
		/// Retrieve string value from AppSettings section in Web.Config
		/// </summary>
		public static string Settings(this HtmlHelper html, string key, string @default = null) {
			string value = ConfigurationManager.AppSettings[key];
			if(key == null) {
				return @default;
			} else {
				return value;
			}
		}
	}
}
