using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Heim.Models;

namespace ShiftRight.Heim.Controllers {

	public class ProjectController: Controller {

		[Authorize]
		public ActionResult Home() {
			ViewBag.Title = "Recent projects";

			return View(
				new ProjectViewModel {
					Projects = new Project[]{
						new Project{
							ID = 1,
							Name = "Rivendel 1",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new HousePlan{
								ID = 1,
								Name = "SMART S1",
								Area = new Area{
									Land = 400,
									Usage = 120
								}
							},
							Created = DateTimeOffset.Parse("2013-12-17T21:22:30Z")
						},
						
						new Project{
							ID = 2,
							Name = "Rivendel 2",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new HousePlan{
								ID = 2,
								Name = "SMART S2",
								Area = new Area{
									Land = 400,
									Usage = 120
								}
							},
							Created = DateTimeOffset.Parse("2013-12-17T21:22:30Z")
						},
						
						new Project{
							ID = 3,
							Name = "Rivendel 3",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new HousePlan{
								ID = 3,
								Name = "SMART S3",
								Area = new Area{
									Land = 400,
									Usage = 120
								}
							},
							Created = DateTimeOffset.Parse("2013-12-17T21:22:30Z")
						},

						new Project{
							ID = 4,
							Name = "Rivendel 4",
							PreviewImage = "/UserData/project_preview_1.jpg",
							Plan = new HousePlan{
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
		public ActionResult Create() {

			return View();
		}

		[Authorize, HttpPost]
		public ActionResult Create(Project project) {

			return View();
		}
	}

}