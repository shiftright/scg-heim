using ShiftRight.Heim.Models;
using ShiftRight.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

	[Authorize]
	public class DesignController : Controller {
		
		public Project CurrentProject {
			get {
				return Session["current_project"] as Project;
			}

			set {
				Session["current_project"] = value;
			}
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext) {

			if(CurrentProject == null && Request != null) {
				var cookie = Request.Cookies["current_project_id"];
				if(cookie != null) {

					int id = int.Parse(cookie.Value);

					using(var dtx = new HeimContext()) {

						var project = dtx.Projects.Include("PlanTemplate.Floors").SingleOrDefault(p => p.ID == id);

						if(project != null) {
							CurrentProject = project;
							Response.SetCookie(new HttpCookie("current_project_id", id.ToString()));
						}
					}
				}
			}

			base.OnActionExecuting(filterContext);
		}
		
		public ActionResult Open(int id) {

			using(var dtx = new HeimContext()) {

				var project = dtx.Projects.Include("PlanTemplate.Floors").SingleOrDefault(p => p.ID == id);

				if(project != null) {
					CurrentProject = project;

					Response.SetCookie(new HttpCookie("current_project_id", id.ToString()));

					var cookie = Request.Cookies["current_project_id"];


					return RedirectToAction("Exterior");
				} else {
					return RedirectToAction("Home", "Projects", null);
				}
			}
		}

		[HttpGet]
		public ActionResult Json(int? projectId = null) {

			Project cp = null;

			if(projectId != null) {
				using(var dtx = new HeimContext()) {
					cp = dtx.Projects.Find(projectId);
				}
			} else {
				cp = CurrentProject;
			}

			if(cp == null) {

				Response.StatusCode = (int)HttpStatusCode.NotFound;
				return Json(new {
					Message = "Project not found"
				}, JsonRequestBehavior.AllowGet);
			}

			var result = new {
				cp.ID,
				cp.Name,
				cp.Created,
				cp.Updated,
				cp.Data
			};

			return Json(result, JsonRequestBehavior.AllowGet);
		}

		[HttpPost]
		public ActionResult Save(Project project) {
			using(var dtx = new HeimContext()) {

				var p = dtx.Projects.Find(project.ID);
				if(p != null) {
					p.Name = project.Name;
					p.Data = project.Data;
					p.Updated = DateTimeOffset.UtcNow;

					dtx.SaveChanges();

					return Json(p);
				} else {
					throw new Exception("Project not found");
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

		public ActionResult CostSummary() {

			var dataTable = new GridViewModel();
			dataTable.Title = "Cost summary";
			dataTable.Columns = new string[] {
				"งาน", "ค่าวัสดุ", "ค่าแรง", "รวม"
			};

			dataTable.Rows = new GridViewRow[]{
				new GridViewRow {
					Items = new string[]{
						"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
					}
				},
				new GridViewRow {
					Items = new string[]{
						"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
					},

					SubTable = new GridViewModel{
						Rows = new GridViewRow[]{
							new GridViewRow {
								Items = new string[]{
									"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
								}
							},
							new GridViewRow {
								Items = new string[]{
									"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
								}
							}
						}
					}
				},
				new GridViewRow {
					Items = new string[]{
						"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
					}
				},
				new GridViewRow {
					Items = new string[]{
						"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
					}
				},
				new GridViewRow {
					Items = new string[]{
						"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
					}
				},
				new GridViewRow {
					Items = new string[]{
						"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
					}
				},
				new GridViewRow {
					Items = new string[]{
						"งานโครงสร้าง", "1,200,000", "600,000", "1,800,000"
					}
				}
			};

			return PartialView("_GridView", dataTable);
		}
	}
}