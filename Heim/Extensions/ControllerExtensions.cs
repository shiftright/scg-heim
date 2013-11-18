using ShiftRight.Heim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftRight.Heim.Extensions {

	public static class ControllerExtensions {
		public static UserProfile GetCurrentUser(this Controller controller) {
			if(controller.User != null && controller.User.Identity.IsAuthenticated) {

				UserProfile user = controller.Session["current_user"] as UserProfile;

				if(user == null) {
					using(var dtx = new HeimContext()) {

						user = dtx.UserProfiles.SingleOrDefault(u => u.Username == controller.User.Identity.Name);
						if(user != null) {
							controller.Session["current_user"] = user;
							return user;
						}
					}
				}

				return user;
			}

			return null;
		}
	}
}