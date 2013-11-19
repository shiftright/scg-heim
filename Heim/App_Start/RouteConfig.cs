using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ShiftRight {
	public class RouteConfig {
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			//routes.MapRoute(
			//	name: "Projects",
			//	url: "{controller}",
			//	defaults: new { controller = "Projects", action = "Home" }
			//);

			routes.MapRoute(
				name: "Default",
				url: "{controller}/{action}/{id}",
				defaults: new { controller = "Projects", action = "Home", id = UrlParameter.Optional }
			);
		}
	}
}