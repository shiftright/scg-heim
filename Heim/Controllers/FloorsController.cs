using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Heim.Models;

namespace ShiftRight.Heim.Controllers {
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

		//
		// POST: /Floors/Create

		[HttpPost]
		public ActionResult Create(Floor floor) {
			if(ModelState.IsValid) {
				context.Floors.Add(floor);
				context.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(floor);
		}

		//
		// GET: /Floors/Edit/5

		public ActionResult Edit(int id) {
			Floor floor = context.Floors.Single(x => x.ID == id);
			return View(floor);
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
			Floor floor = context.Floors.Single(x => x.ID == id);
			return View(floor);
		}

		//
		// POST: /Floors/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id) {
			Floor floor = context.Floors.Single(x => x.ID == id);
			context.Floors.Remove(floor);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		protected override void Dispose(bool disposing) {
			if(disposing) {
				context.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}