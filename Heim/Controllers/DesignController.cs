using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftRight.Heim.Controllers {
	public class DesignController : Controller {

		public ActionResult Index() {
			return RedirectToAction("Exterior");
		}

		public ActionResult Exterior() {
			return View();
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