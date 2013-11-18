﻿using System;
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

		[Authorize]
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
		public ActionResult Customize(int planId) {
			
			using(var dtx = new HeimContext()) {

				var plan = dtx.Plans.Include("Floors").Include("Floors.Variants").Single(p => p.ID == planId);

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
					if(item.Variants.Count() > 0) {

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

		[Authorize, HttpPost]
		public ActionResult SaveCustomize(Project project) {

			using(var dtx = new HeimContext()) {

				var user = dtx.UserProfiles.Single(u => u.Username == User.Identity.Name);

				project.ID = 0;
				project.OwnerID = user.ID;
				project.Created = DateTimeOffset.UtcNow;
				project.Updated = DateTimeOffset.UtcNow;
				project.IsDeleted = false;
				project.Name = project.Name.Trim();

				//project.Floors
				//project.Data = ;

				dtx.Projects.Add(project);
				dtx.SaveChanges();
				
				return Json(project);
			}
		}
	}

}