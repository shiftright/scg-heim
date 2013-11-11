using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Heim.Models;

namespace ShiftRight.Heim.Controllers {
	public class UserProfilesController : Controller {
		private HeimContext context = new HeimContext();

		//
		// GET: /UserProfiles/

		public ViewResult Index() {
			return View(context.UserProfiles.ToList());
		}

		//
		// GET: /UserProfiles/Details/5

		public ViewResult Details(int id) {
			UserProfile userprofile = context.UserProfiles.Single(x => x.ID == id);
			return View(userprofile);
		}

		//
		// GET: /UserProfiles/Create

		public ActionResult Create() {
			return View();
		}

		//
		// POST: /UserProfiles/Create

		[HttpPost]
		public ActionResult Create(UserProfile userprofile) {
			if(ModelState.IsValid) {
				context.UserProfiles.Add(userprofile);
				context.SaveChanges();
				return RedirectToAction("Index");
			}

			return View(userprofile);
		}

		//
		// GET: /UserProfiles/Edit/5

		public ActionResult Edit(int id) {
			UserProfile userprofile = context.UserProfiles.Single(x => x.ID == id);
			return View(userprofile);
		}

		//
		// POST: /UserProfiles/Edit/5

		[HttpPost]
		public ActionResult Edit(UserProfile userprofile) {
			if(ModelState.IsValid) {
				context.Entry(userprofile).State = EntityState.Modified;
				context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(userprofile);
		}

		//
		// GET: /UserProfiles/Delete/5

		public ActionResult Delete(int id) {
			UserProfile userprofile = context.UserProfiles.Single(x => x.ID == id);
			return View(userprofile);
		}

		//
		// POST: /UserProfiles/Delete/5

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id) {
			UserProfile userprofile = context.UserProfiles.Single(x => x.ID == id);
			context.UserProfiles.Remove(userprofile);
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