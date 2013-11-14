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
			var vm = new NewProjectViewModel {
				Search = search,
				Plans = new PlanViewModel[]{
					new PlanViewModel{
						ID = 1,
						Name = "SMART S1",
						PreviewImage = "/UserData/project_preview_1.jpg",
						Area = new Area{
							Land = 100,
							Usage = 50
						}
					},
					
					new PlanViewModel{
						ID = 2,
						Name = "SMART S3",
						PreviewImage = "/UserData/project_preview_1.jpg",
						Area = new Area{
							Land = 100,
							Usage = 50
						}
					},
					
					new PlanViewModel{
						ID = 3,
						Name = "SMART S3",
						PreviewImage = "/UserData/project_preview_1.jpg",
						Area = new Area{
							Land = 100,
							Usage = 50
						}
					},
					
					new PlanViewModel{
						ID = 4,
						Name = "SMART S4",
						PreviewImage = "/UserData/project_preview_1.jpg",
						Area = new Area{
							Land = 100,
							Usage = 50
						}
					},
					
					new PlanViewModel{
						ID = 5,
						Name = "SMART S5",
						PreviewImage = "/UserData/project_preview_1.jpg",
						Area = new Area{
							Land = 100,
							Usage = 50
						}
					}
				}
			};

			return View(vm);
		}

		[Authorize]
		public ActionResult Customize(Project project) {

			if(project.Plan != null) {

				using(var dtx = new HeimContext()) {


					//HousePlan plan = dtx.Plans.Find(project.Plan.ID);

					//if(plan != null) {
					//	project.Plan = plan;
					//	ViewBag.Title = project.Plan.Name;
					//} else {
#if DEBUG
					project.Plan.Name = "SMART S" + project.Plan.ID;
					ViewBag.Title = project.Plan.Name;
#endif
					//}
				}

			}
			
			return View(new CustomizeProjectViewModel {
				ID = project.ID,
				Name = project.Name,
				PreviewImage = "/UserData/scg_th01_04floorplan-1.jpg",

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
				},

				#endregion

				Plan = new PlanViewModel {
					ID = project.Plan.ID,
					Name = project.Plan.Name,
					PreviewImage = "/UserData/scg_th01_04floorplan-1.jpg",

					Floors = new FloorViewModel[]{
						new FloorViewModel{
							ID = 1,
							FloorNumber = 1,
							PlanPreviewImage = "/UserData/scg_th01_04floorplan-1.png",

							Variants = new FloorVariantViewModel[]{
								new FloorVariantViewModel{
									ID = 11,
									Name = "Variant A",
									FloorNumber = 1,
									IsDefault = true,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-1.png"
								},

								new FloorVariantViewModel{
									ID = 12,
									Name = " Variant B",
									FloorNumber = 1,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-2.png"
								},

								new FloorVariantViewModel{
									ID = 13,
									Name = " Variant C",
									FloorNumber = 1,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-1.png"
								},

								new FloorVariantViewModel{
									ID = 14,
									Name = " Variant D",
									FloorNumber = 1,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-1.png"
								}
							},
						},

						new FloorViewModel{
							ID = 2,
							FloorNumber = 2,
							PlanPreviewImage = "/UserData/scg_th01_04floorplan-1.png",
							Variants = new FloorVariantViewModel[]{
								new FloorVariantViewModel{
									ID = 21,
									Name = "Variant A",
									FloorNumber = 2,
									IsDefault = true,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-1.png"
								},

								new FloorVariantViewModel{
									ID = 22,
									Name = "Variant B",
									FloorNumber = 2,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-2.png"
								}
							},
						},

						new FloorViewModel{
							ID = 3,
							FloorNumber = 3,
							PlanPreviewImage = "/UserData/scg_th01_04floorplan-1.png",
							Variants = new FloorVariantViewModel[]{
								new FloorVariantViewModel{
									ID = 31,
									Name = "Variant A",
									FloorNumber = 3,
									IsDefault = true,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-1.png"
								},

								new FloorVariantViewModel{
									ID = 32,
									Name = "Variant B",
									FloorNumber = 3,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-2.png"
								},

								new FloorVariantViewModel{
									ID = 33,
									Name = "Variant C",
									FloorNumber = 3,
									PlanPreviewImageFilePath = "/UserData/scg_th01_04floorplan-1.png"
								}
							},
						}
					}
				}
			});
		}
	}

}