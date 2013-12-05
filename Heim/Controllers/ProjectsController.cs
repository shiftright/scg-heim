using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Heim.Models;
using ShiftRight.Heim.Extensions;

namespace ShiftRight.Heim.Controllers {

	public class ProjectIndexViewModel {
		public IEnumerable<ProjectViewModel> Projects { get; set; }
	}

	[Authorize]
	public class ProjectsController : Controller {

		public Project CurrentProject {
			get {
				return Session["current_project"] as Project;
			}

			set {
				Session["current_project"] = value;
			}
		}

		public ActionResult Index() {

			using(var dtx = new HeimContext()) {

				var query = from p in dtx.Projects
							orderby p.Created descending
							select new ProjectViewModel {
								ID = p.ID,
								Created = p.Created,
								Name = p.Name,
								Owner = p.Owner
							};

				return View(new ProjectIndexViewModel {
					Projects = query.ToList()
				});
			}

		}

		public ActionResult Home() {

			using(var dtx  = new HeimContext()) {

				var user = this.GetCurrentUser();

				var query = from project in dtx.Projects
							where project.OwnerID == user.ID
							orderby project.Updated descending
							select new ProjectViewModel {
								ID = project.ID,
								Created = project.Created,
								Name = project.Name,
								//PreviewImage = project.
								Plan = new PlanViewModel {
									Area = project.PlanTemplate.Area,
									Name = project.PlanTemplate.Name,
									PreviewImage = project.PlanTemplate.PreviewImageFilePath
								}
							};

				var list = query.ToList();
				list.ForEach(x => x.PreviewImage = x.Plan.PreviewImage);

				return View(
					new ProjectHomeViewModel {
						Projects = list
				});
			}
		}

		public ActionResult New(string search) {

			ViewBag.Title = "Select house plan";

			using(var dtx = new HeimContext()) {

				if(!String.IsNullOrWhiteSpace(search)) {
					search = search.Trim().ToLower();
				} else {
					search = null;
				}

				var query = from item in dtx.Plans
							where (search == null || item.Name.ToLower().Contains(search)) && item.Floors.Count() > 0
							orderby item.Name
							select new PlanViewModel {
								ID = item.ID,
								Name = item.Name,
								PreviewImage = item.PreviewImageFilePath,
								Area = item.Area
							};

				NewProjectViewModel vm = new NewProjectViewModel {
					Search = search,
					Plans = query.ToList()
				};

				return View(vm);

			}

		}

		public ActionResult Customize(int planId) {
			
			using(var dtx = new HeimContext()) {

				var plan = dtx.Plans.Include("Floors").Include("Attributes").Include("Floors.Variants").Single(p => p.ID == planId);

				if(plan == null) {
					return RedirectToAction("New");
				}

				ViewBag.Title = plan.Name;

				var vm = new CustomizeProjectViewModel {
					Name = plan.Name,
					PreviewImage = plan.PreviewImageFilePath,

					Plan = new PlanViewModel {
						ID = plan.ID,
						Name = plan.Name,
						PreviewImage = plan.PreviewImageFilePath,
						Floors = plan.Floors.Select(fl =>
							new FloorViewModel {
								ID = fl.ID,
								FloorNumber = fl.FloorNumber,
								Variants = fl.Variants.Select(vr =>
									new FloorVariantViewModel {
										ID = vr.ID,
										Name = vr.Name,
										IsDefault = false,
										PlanPreviewImageFilePath = vr.PlanPreviewImageFilePath
									}).ToList()
							}).ToList(),
					},

					Attributes = plan.Attributes.ToList()
				};

				foreach(var item in vm.Plan.Floors) {
					if(item.Variants.Count() > 0) {

						foreach(var vr in item.Variants) {
							vr.IsDefault = true;
							item.PlanPreviewImage = vr.PlanPreviewImageFilePath;

							break;
						}
					}
				}

				//vm.Attributes.Add(new ShiftRight.Heim.Models.Attribute {
				//	ID = 0,
				//	Name = "พื้นที่ใช้สอย",
				//	Unit = 
				//});

				return View(vm);

			}// end using
		}

		[HttpPost]
		public ActionResult SaveCustomize(ProjectViewModel model) {

			using(var dtx = new HeimContext()) {

				var user = dtx.UserProfiles.Single(u => u.Username == User.Identity.Name);
				var selectedVariants = model.Floors.Select(f => f.ID).ToArray();
				var variants = dtx.FloorVariants
					.Where(fv => selectedVariants.Contains(fv.ID))
					.Select(fv => new {
						fv.Floor.FloorNumber,
						ModelName = "fl_" + fv.Floor.FloorNumber,
						fv.ModelFilePath,
						fv.Name,
						fv.ID,

						Assets = new Asset[]{ }
					}).ToList();

				var project = new Project();

				project.ID = 0;
				project.OwnerID = user.ID;
				project.Created = DateTimeOffset.UtcNow;
				project.Updated = DateTimeOffset.UtcNow;
				project.IsDeleted = false;
				project.PlanTemplateID = model.Plan.ID;
				project.Name = model.Name.Trim();
				project.Data = System.Web.Helpers.Json.Encode(new {
					Floors = variants.Select(fv => new {
						fv.FloorNumber,
						ModelFilePath = String.IsNullOrEmpty(fv.ModelFilePath)? null: VirtualPathUtility.ToAbsolute(fv.ModelFilePath),
						fv.Name,
						fv.ID
					}).ToArray()
				});

				//project.Floors
				//project.Data = ;

				dtx.Projects.Add(project);
				dtx.SaveChanges();
				
				return Json(project);
			}
		}

		public ActionResult Delete(int id) {

			using(var dtx = new HeimContext()) {
				Project project = dtx.Projects.Single(x => x.ID == id);
				return View(project);
			}
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id) {

			using(var dtx = new HeimContext()) {

				Project project = dtx.Projects.Single(x => x.ID == id);
				dtx.Projects.Remove(project);
				dtx.SaveChanges();

				return RedirectToAction("Index");
			}
		}
	}

}