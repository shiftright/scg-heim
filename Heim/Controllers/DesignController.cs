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

		public IEnumerable<IGrouping<AssetType, AssetViewModel>> Assets { get; set; }

		public IEnumerable<Floor> Floors { get; set; }
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

		public ActionResult Open(int id) {

			using(var dtx = new HeimContext()) {

				var project = dtx.Projects.Include("PlanTemplate.Floors").SingleOrDefault(p => p.ID == id);

				if(project != null) {
					CurrentProject = project;
					return RedirectToAction("Exterior");
				} else {
					return RedirectToAction("Home", "Projects", null);
				}
			}
		}

		public ActionResult Exterior() {

			if(CurrentProject == null) {
				return RedirectToAction("Home", "Projects");
			}

			using(var dtx = new HeimContext()) {

				Project project = CurrentProject;

				var query = from asset in dtx.Assets
							orderby asset.Name
							select asset;

				var groups = from asset in query.ToList()
							 group new AssetViewModel{
								ID = asset.ID,
								Name = asset.Name,
								Mapping = asset.Mapping,
								Created = asset.Created,
								Updated = asset.Updated,
								PreviewFilePath = asset.PreviewFilePath,
								AssetType = asset.AssetType,
								AssetFilePath = asset.AssetFilePath,
							} by asset.AssetType into assetGroup
							select assetGroup;
				groups = groups.ToList();

				foreach(var group in groups) {
					foreach(var item in group) {
						item.Selected = true;
						break;
					}
				}

				var vm = new ExteriorViewModel {
					ProjectName = project.Name,
					ProjectID = project.ID,
					Assets = groups,
					Floors = project.PlanTemplate.Floors
				};

				return View(vm);
			}
		}

		public ActionResult Furniture() {
			return Exterior();
		}

		public ActionResult Wallpaper() {
			return Exterior();
		}

		public ActionResult Electricity() {
			return Exterior();
		}
	}
}