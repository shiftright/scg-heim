using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Heim.Models;

namespace ShiftRight.Heim.Controllers {

	public class ProjectIndexViewModel {
		public IEnumerable<ProjectViewModel> Projects { get; set; }
	}

	public class ProjectsController : Controller {


		[Authorize]
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

		[Authorize]
		public ActionResult Home() {
			ViewBag.Title = "Recent projects";

			return View(
				new ProjectHomeViewModel {
					Projects = new ProjectViewModel[]{
						new ProjectViewModel{
							ID = 1,
							Name = "Rivendell 1",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new PlanViewModel{
								ID = 1,
								Name = "SMART S1",
								Area = new Area{
									Land = 400,
									Usage = 120
								}
							},
							Created = DateTimeOffset.Parse("2013-12-17T21:22:30Z")
						},
						
						new ProjectViewModel{
							ID = 2,
							Name = "Rivendell 2",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new PlanViewModel{
								ID = 2,
								Name = "SMART S2",
								Area = new Area{
									Land = 400,
									Usage = 120
								}
							},
							Created = DateTimeOffset.Parse("2013-12-17T21:22:30Z")
						},
						
						new ProjectViewModel{
							ID = 3,
							Name = "Rivendell 3",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new PlanViewModel{
								ID = 3,
								Name = "SMART S3",
								Area = new Area{
									Land = 400,
									Usage = 120
								}
							},
							Created = DateTimeOffset.Parse("2013-12-17T21:22:30Z")
						},

						new ProjectViewModel{
							ID = 4,
							Name = "Rivendell 4",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new PlanViewModel{
								ID = 4,
								Name = "SMART S4",
								Area = new Area{
									Land = 400,
									Usage = 120
								}
							},
							Created = DateTimeOffset.Parse("2013-12-17T21:22:30Z")
						}
					}
				});
		}

		[Authorize, HttpGet]
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

		[Authorize]
		public ActionResult Customize(Project project) {

			if(project.Plan != null) {

				using(var dtx = new HeimContext()) {

					var plan = dtx.Plans.Include("Floors").Include("Floors.Variants").Single(p => p.ID == project.Plan.ID);

					project.Name = project.Plan.Name;
					ViewBag.Title = project.Plan.Name;

					var vm = new CustomizeProjectViewModel {
						ID = project.ID,
						Name = project.Name,
						PreviewImage = plan.PreviewImageFilePath,

						Plan = new PlanViewModel {
							ID = project.Plan.ID,
							Name = project.Plan.Name,
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

						#region Attributes

						Attributes = new ShiftRight.Heim.Models.Attribute[]{
							new ShiftRight.Heim.Models.Attribute{
								ID = 1,
								Name = "ช่วงราคา",
								Value = "5,000,000",
								Unit = "บาท"
							},
					
							new ShiftRight.Heim.Models.Attribute{
								ID = 2,
								Name = "พื้นที่ใช้สอย",
								Value = "220",
								Unit = "ตารางเมตร"
							},
					
							new ShiftRight.Heim.Models.Attribute{
								ID = 3,
								Name = "ที่ดิน",
								Value = "13.5 x 16",
								Unit = "ตารางเมตร"
							},
					
							new ShiftRight.Heim.Models.Attribute{
								ID = 4,
								Name = "ห้องนอน",
								Value = "2-4",
								Unit = "ห้อง"
							}
						}

						#endregion
					};

					foreach(var item in vm.Plan.Floors) {
						if(item.Variants.Count() > 0){

							foreach(var vr in item.Variants) {
								vr.IsDefault = true;
								item.PlanPreviewImage = vr.PlanPreviewImageFilePath;

								break;
							}
						}
					}

					return View(vm);

				}// end using
			}
				return View();
		}
	}

}