using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Heim.Models;

namespace ShiftRight.Heim.Controllers {

	[Authorize]
	public class FloorsController : Controller {
		private HeimContext context = new HeimContext();

		//
		// GET: /Floors/

		public ViewResult Index() {
			return View(context.Floors.ToList());
		}

		//
		// GET: /Floors/Details/5

		public ViewResult Details(int id) {
			Floor floor = context.Floors.Single(x => x.ID == id);
			return View(floor);
		}

		//
		// GET: /Floors/Create

		public ActionResult Create() {
			return View();
		}

		[HttpPost]
		public ActionResult Create(Floor floor) {

			if(ModelState.IsValid) {
				context.Floors.Add(floor);
				context.SaveChanges();

				return RedirectToAction("Edit", new { id = floor.ID });
			}

			return View(floor);
		}

		public ActionResult CreateBlank(int planId) {

			using(var dtx = new HeimContext()) {

				int? floorNumber = dtx.Floors.Where(f => f.PlanID == planId).Max(f => (int?)f.FloorNumber);

				var floor = new Floor {
					PlanID = planId,
					FloorNumber = floorNumber.HasValue? + floorNumber.Value + 1: 1
				};

				dtx.Floors.Add(floor);
				dtx.SaveChanges();

				return RedirectToAction("Edit", new { id = floor.ID });
			}

		}

		//
		// GET: /Floors/Edit/5

		public ActionResult Edit(int id) {

			using(var dtx = new HeimContext()) {

				var query = from floor in dtx.Floors
							where floor.ID == id
							select new FloorViewModel {
								ID = floor.ID,
								FloorNumber = floor.FloorNumber,
								Plan = new PlanViewModel{
									ID = floor.PlanID,
									Name = floor.Plan.Name
								},
								Variants = floor.Variants.Select(v => new FloorVariantViewModel {
									ID = v.ID,
									FloorNumber = v.Floor.FloorNumber,
									Name = v.Name,
									Created = v.Created,
									Updated = v.Updated
								})
							};

				var f = query.Single();
				return View(f);
			}
		}

		//
		// POST: /Floors/Edit/5

		[HttpPost]
		public ActionResult Edit(Floor floor) {
			if(ModelState.IsValid) {
				context.Entry(floor).State = EntityState.Modified;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(floor);
		}

		//
		// GET: /Floors/Delete/5

		public ActionResult Delete(int id) {

			using(var dtx = new HeimContext()) {

				var query = from fl in dtx.Floors
							where fl.ID == id
							select new FloorViewModel {
								ID = fl.ID,
								FloorNumber = fl.FloorNumber,
								Plan = new PlanViewModel {
									ID = fl.PlanID,
									Name = fl.Plan.Name
								}
							};

				return View(query.Single());
			}
		}

		//
		// POST: /Floors/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id) {
			Floor floor = context.Floors.Single(x => x.ID == id);
			context.Floors.Remove(floor);
			context.SaveChanges();
			return RedirectToAction("Edit", "Plans", new { id = floor.PlanID });
		}

		protected override void Dispose(bool disposing) {
			if(disposing) {
				context.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}