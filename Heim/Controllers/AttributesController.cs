using ShiftRight.Heim.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShiftRight.Heim.Controllers {

	[Authorize]
	public class AttributesController : Controller {
		
		//public ActionResult Index() {
		//	return View();
		//}

		public ActionResult Edit(int id) {
			using(var dtx = new HeimContext()) {
				var attr = dtx.Attributes.Find(id);

				return View(attr);
			}
		}

		[HttpPost]
		public ActionResult Edit(ShiftRight.Heim.Models.Attribute attr) {
			using(var dtx = new HeimContext()) {

				if(this.ModelState.IsValid) {
					dtx.Entry<ShiftRight.Heim.Models.Attribute>(attr).State = System.Data.Entity.EntityState.Modified;
					dtx.SaveChanges();
				}

				return RedirectToAction("Edit", "Plans", new { id = attr.PlanID });
			}
		}

		public ActionResult Delete(int id) {

			using(var dtx = new HeimContext()) {
				ShiftRight.Heim.Models.Attribute attr = dtx.Attributes.Single(x => x.ID == id);
				return View(attr);
			}
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id) {

			using(var dtx = new HeimContext()) {

				ShiftRight.Heim.Models.Attribute attr = dtx.Attributes.Single(x => x.ID == id);
				dtx.Attributes.Remove(attr);
				dtx.SaveChanges();
				return RedirectToAction("Edit", "Plans", new { id = attr.PlanID});
			}
		}
	}
}