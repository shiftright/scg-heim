using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Common;
using ShiftRight.Heim.Models;
using ShiftRight.Web.Attributes.Validation;
using ShiftRight.Web.Extensions;
using ShiftRight.Common.Extensions;

namespace ShiftRight.Heim.Controllers {

	public class PlansController : Controller {

		private HeimContext context = new HeimContext();

		public ViewResult Index() {
			return View(context.Plans.Include(plan => plan.Floors).ToList());
		}

		public ActionResult Create() {
			return View();
		}

		[HttpPost]
		public ActionResult Create(PlanViewModel plan) {
			if(ModelState.IsValid) {

				var newPlan = new Plan {
					Created = DateTimeOffset.UtcNow,
					Updated = DateTimeOffset.UtcNow,
					Name = plan.Name.Trim(),
					Area = plan.Area
				};
				context.Plans.Add(newPlan);
				context.SaveChanges();

				plan.ID = newPlan.ID;

				if(plan.PreviewImageFile != null || plan.ModelFilePath != null) {

					string root = ConfigurationManager.AppSettings["UserDataRoot"];
					root = Path.Combine(root, "plans", newPlan.ID.ToString());

					IStorage storage = this.GetStorage(Server.MapPath(root));
					storage.EnsureRootExist();

					// Preview
					string fileName = newPlan.Updated.UtcTicks.ToString();

					if(plan.PreviewImageFile != null && plan.PreviewImageFile.ContentLength > 0 || plan.ModelFile != null && plan.ModelFile.ContentLength > 0) {
						string ext = plan.PreviewImageFile.InputStream.GetFileExtension();
						ext = ext == null ? null : ext.ToLower();

						storage.Save(plan.PreviewImageFile.InputStream, fileName + ext);

						newPlan.PreviewImageFilePath = Path.Combine(root, fileName + ext);
					}

					if(plan.ModelFile != null && plan.ModelFile.ContentLength > 0) {
						string ext = plan.ModelFile.InputStream.GetFileExtension();
						ext = ext == null ? null : ext.ToLower();

						storage.Save(plan.ModelFile.InputStream, fileName + ext);

						newPlan.ModelFilePath = Path.Combine(root, fileName + ext);
					}

					context.Entry<Plan>(newPlan).State = EntityState.Modified;
					context.SaveChanges();
				}
				

				return RedirectToAction("Edit", new { ID = newPlan.ID });
			}

			return View(plan);
		}

		public ActionResult Edit(int id) {

			using(var dtx =  new HeimContext()) {

				var query = from plan in dtx.Plans
							where plan.ID == id
							select new PlanViewModel {
								ID = plan.ID,
								Name = plan.Name,
								Area = plan.Area,
								PreviewImage = plan.PreviewImageFilePath,
								ModelFilePath = plan.ModelFilePath,
								Attributes = plan.Attributes,
								Floors = plan.Floors.Select(fl => new FloorViewModel {
									ID = fl.ID,
									FloorNumber = fl.FloorNumber
								})
							};

				return View(query.Single());
			}

		}

		[HttpPost]
		public ActionResult Edit(PlanViewModel plan) {

			using(var dtx = new HeimContext()) {
				var planModel = dtx.Plans.Include("Floors").Single( s => s.ID == plan.ID );

				if(ModelState.IsValid) {
					planModel.Updated = DateTimeOffset.UtcNow;
					planModel.Name = plan.Name.Trim();
					
					IStorage storage = null;
					string root = null;

					// prepare storage
					if(plan.PreviewImageFile != null && plan.PreviewImageFile.ContentLength > 0 || plan.ModelFile != null && plan.ModelFile.ContentLength > 0) {

						string fileName = planModel.Updated.UtcTicks.ToString();
						root = ConfigurationManager.AppSettings["UserDataRoot"];
						root = Path.Combine(root, "plans", plan.ID.ToString());

						storage = this.GetStorage(Server.MapPath(root));
						storage.EnsureRootExist();

						if(plan.PreviewImageFile != null && plan.PreviewImageFile.ContentLength > 0) {
							string ext = plan.PreviewImageFile.InputStream.GetFileExtension();
							ext = ext == null ? null : ext.ToLower();

							storage.Save(plan.PreviewImageFile.InputStream, fileName + ext);

							planModel.PreviewImageFilePath = Path.Combine(root, fileName + ext);
						}

						if(plan.ModelFile != null && plan.ModelFile.ContentLength > 0) {
							string ext = plan.ModelFile.InputStream.GetFileExtension();
							ext = ext == null ? null : ext.ToLower();

							storage.Save(plan.ModelFile.InputStream, fileName + ext);

							planModel.ModelFilePath = Path.Combine(root, fileName + ext);
						}
					}


					context.Entry<Plan>(planModel).State = EntityState.Modified;
					context.SaveChanges();

					if(plan.PreviewImageFile == null) {
						return RedirectToAction("Index");
					}
				}

				plan.Floors = planModel.Floors.Select(fl => new FloorViewModel {
					ID = fl.ID,
					FloorNumber = fl.FloorNumber
				});

				return View(plan);
			}
		}

		public ActionResult Delete(int id) {
			Plan plan = context.Plans.Single(x => x.ID == id);
			return View(plan);
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id) {
			Plan plan = context.Plans.Single(x => x.ID == id);
			context.Plans.Remove(plan);
			context.SaveChanges();
			return RedirectToAction("Index");
		}

		[HttpPost]
		public ActionResult Variant(int planID, int id) {
			return View();
		}

		public ActionResult AddAttribute(ShiftRight.Heim.Models.Attribute attr) {
			using(var dtx = new HeimContext()) {

				if(ModelState.IsValid) {
					dtx.Attributes.Add(attr);
					dtx.SaveChanges();
				}

				return RedirectToAction("Edit", new { id = attr.PlanID });
			}
		}

		protected override void Dispose(bool disposing) {
			if(disposing) {
				context.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}