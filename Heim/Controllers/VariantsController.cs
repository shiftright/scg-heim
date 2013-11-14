using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Heim.Models;
using ShiftRight.Web.Extensions;
using ShiftRight.Common;
using System.Configuration;
using System.IO;
using ShiftRight.Common.Extensions;

namespace ShiftRight.Heim.Controllers {

	[Authorize]
	public class VariantsController : Controller {

		public ActionResult Edit(int id) {

			using(var dtx = new HeimContext()) {

				var query = from v in dtx.FloorVariants
							where v.ID == id
							select new FloorVariantViewModel {
								ID = v.ID,
								FloorID = v.FloorID,
								PlanID = v.Floor.PlanID,
								FloorNumber = v.Floor.FloorNumber,
								Created = v.Created,
								Updated = v.Updated,
								Name = v.Name,
								PlanPreviewImageFilePath = v.PlanPreviewImageFilePath,
								ModelFilePath = v.ModelFilePath
							};

				var variant = query.Single();

				if(!String.IsNullOrEmpty(variant.ModelFilePath)){
					string modelFilePath = Server.MapPath(variant.ModelFilePath);
					if(System.IO.File.Exists(modelFilePath)) {
						variant.ModelFileSize = Math.Floor(new FileInfo(modelFilePath).Length / 1024.0);
					}
				}
				return View(variant);
			}
		}

		[HttpPost]
		public ActionResult Edit(FloorVariantViewModel variant) {

			DateTimeOffset updated = DateTimeOffset.UtcNow;

			using(var dtx = new HeimContext()) {
				if(ModelState.IsValid) {

					var fv = dtx.FloorVariants.Single(v => v.ID == variant.ID);
					fv.Name = variant.Name.Trim();
					fv.Updated = updated;


					IStorage storage = null;
					string root = null;

					// prepare storage
					if(variant.PlanImageFile != null || variant.ModelFile != null) {
						
						root = ConfigurationManager.AppSettings["UserDataRoot"];
						root = Path.Combine(root, "plans",  variant.PlanID.ToString(), "variants", variant.ID.ToString());

						storage = this.GetStorage(Server.MapPath(root));
						storage.EnsureRootExist();
					}

					string ext = "";

					if(variant.PlanImageFile != null) {
						ext = variant.PlanImageFile.InputStream.GetFileExtension();
						ext = ext == null? null : ext.ToLower();

						fv.PlanPreviewImageFilePath = Path.Combine(root, fv.Updated.Ticks.ToString() + ext);

						storage.Save(variant.PlanImageFile.InputStream, fv.Updated.Ticks.ToString() + ext);
					}

					if(variant.ModelFile != null) {

						ext = variant.ModelFile.InputStream.GetFileExtension();
						ext = ext == null ? null : ext.ToLower();
						
						// add allowed file extensions here
						if(ext == ".zip") {
							fv.ModelFilePath = Path.Combine(root, fv.Updated.Ticks.ToString() + ext);
							storage.Save(variant.ModelFile.InputStream, fv.Updated.Ticks.ToString() + ext);
						}
					}

					dtx.SaveChanges();

					return RedirectToAction("Edit", new { id = variant.ID });
				}
			}

			return View(variant);
		}

		public ActionResult CreateBlank(int floorId) {

			using(var dtx = new HeimContext()) {

				var floor = dtx.Floors.Find(floorId);
				int variantCount = dtx.FloorVariants.Where(fv => fv.FloorID == floorId).Count();

				var variant = new FloorVariant {
					Created = DateTimeOffset.UtcNow,
					Updated = DateTimeOffset.UtcNow,
					Name = "Variant " + (variantCount + 1),
					FloorID = floorId,
				};

				floor.Variants.Add(variant);
				dtx.SaveChanges();

				return RedirectToAction("Edit", new { id = variant.ID, create = true });
			}
		}

		public ActionResult Delete(int id) {

			using(var dtx = new HeimContext()) {

				var variant = dtx.FloorVariants.Single(v => v.ID == id);

				dtx.FloorVariants.Remove(variant);
				dtx.SaveChanges();

				return RedirectToAction("Edit", "Floors", new { id = variant.FloorID });
			}
		}
	}
}