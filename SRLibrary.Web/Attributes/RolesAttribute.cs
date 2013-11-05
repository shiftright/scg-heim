using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ShiftRight.Web {
	public class RolesAttribute : AuthorizeAttribute {
		public RolesAttribute() {

		}

		public RolesAttribute(string roles) {
			base.Roles = roles;
		}

		public override void OnAuthorization(AuthorizationContext filterContext) {

			if(filterContext.HttpContext.User != null) {
				if(filterContext.HttpContext.User.Identity.IsAuthenticated) {
					if(filterContext.HttpContext.User.Identity is FormsIdentity) {
						FormsIdentity id = (FormsIdentity)filterContext.HttpContext.User.Identity;
						FormsAuthenticationTicket ticket = id.Ticket;

						// Get the stored user-data, in this case, our roles
						var index = ticket.UserData.IndexOf("\"Roles\":") + 9;
						var stop = ticket.UserData.IndexOf("\"", index);
						string userData = ticket.UserData.Substring(index, stop - index);
						string[] roles = userData.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
							.Select(r => r.Trim()).ToArray();
						HttpContext.Current.User = new GenericPrincipal(id, roles);
					}
				}
			}

			base.OnAuthorization(filterContext);
		}

		protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext) {

			if(filterContext.HttpContext != null) {
				if(filterContext.HttpContext.User.Identity.IsAuthenticated) {

					filterContext.Result = new RedirectResult("/Errors/Unauthorize");
					return;
				}
			}

			base.HandleUnauthorizedRequest(filterContext);

		}
	}
}