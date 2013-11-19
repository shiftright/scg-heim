using ShiftRight.Heim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftRight.Heim.Controllers {

	public class ExteriorViewModel {
		public string ProjectName { get; set; }
		public int ProjectID { get; set; }

		public int PlanID { get; set; }
		public string PlanName { get; set; }
	}

	public class DesignController : Controller {
		
		public Project CurrentProject {
			get {
				return Session["current_project"] as Project;
			}

			set {
				Session["current_project"] = value;
			}
		}

		public ActionResult Index() {
			return RedirectToAction("Exterior");
		}

		public ActionResult Exterior() {

			if(CurrentProject == null) {
				return RedirectToAction("Home", "Projects");
			}

			using(var dtx = new HeimContext()) {

				Project project = CurrentProject;

				var vm = new ExteriorViewModel {
					ProjectName = project.Name,
					ProjectID = project.ID
				};

				return View(vm);
			}

		}
		public ActionResult Furniture() {
			return View();
		}
		public ActionResult Painting() {
			return View();
		}
		public ActionResult Electricity() {
			return View();
		}
	}
}