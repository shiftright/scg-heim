using ShiftRight.Common;
using ShiftRight.Heim.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShiftRight.Web.Extensions;
using ShiftRight.Common.Extensions;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShiftRight.Heim.Controllers {

	public class AssetViewModel : Asset {

		[NotMapped]
		[DisplayName("Preview image file")]
		public HttpPostedFileBase PreviewImageFile { get; set; }

		[NotMapped]
		[DisplayName("Asset file")]
		public HttpPostedFileBase AssetFile { get; set; }
	}

	[Authorize]
	public class AssetsController : Controller {

		public ActionResult Index() {

			using(var dtx = new HeimContext()) {
				 				var query = from mat in dtx.Assets
							orderby mat.AssetType, mat.Name
							group mat by mat.AssetType into matgroup
							select matgroup;

				return View(query.ToList());
			}
		}


		public ActionResult Create() {
			return View();
		}

		[HttpPost]
		public ActionResult Create([Bind(Exclude = "ID")] AssetViewModel mat) {

			using(var dtx = new HeimContext()) {

				if(ModelState.IsValid) {

					mat.Created = DateTimeOffset.UtcNow;
					mat.Updated = mat.Created;

					dtx.Assets.Add(mat as Asset);
					dtx.SaveChanges();

					SaveAssetFiles(dtx, mat);

					dtx.Entry<Asset>(mat as Asset).State = EntityState.Modified;
					dtx.SaveChanges();
				}

				return View(mat);
			}
		}

		public ActionResult Edit(int id) {
			using(var dtx = new HeimContext()) {

				var asset = dtx.Assets.Find(id);

				return View(new AssetViewModel {
					ID = asset.ID,
					Name = asset.Name,
					Mapping = asset.Mapping,
					Created = asset.Created,
					Updated = asset.Updated,
					PreviewFilePath = asset.PreviewFilePath,
					AssetType = asset.AssetType,
					AssetFilePath = asset.AssetFilePath,
				});
			}
		}

		[HttpPost]
		public ActionResult Edit(AssetViewModel asset) {
			using(var dtx = new HeimContext()) {

				asset.Updated = DateTimeOffset.UtcNow;

				SaveAssetFiles(dtx, asset);

				dtx.Entry<Asset>(asset).State = EntityState.Modified;
				dtx.SaveChanges();

				return View(asset);
			}
		}

		public ActionResult Delete(int id) {

			using(var dtx = new HeimContext()) {

				var asset = dtx.Assets.Find(id);

				return View(asset);
			}
		}

		[HttpPost, ActionName("Delete")]
		public ActionResult DeleteConfirmed(int id) {

			using(var dtx = new HeimContext()) {

				Asset asset = dtx.Assets.Single(x => x.ID == id);
				dtx.Assets.Remove(asset);
				dtx.SaveChanges();
				return RedirectToAction("Index");
			}
		}

		private void SaveAssetFiles(HeimContext dtx, AssetViewModel mat) {

			// Save Material file and Preview file
			IStorage storage = null;
			string root = null;

			// prepare storage
			if(mat.PreviewImageFile != null || mat.AssetFile != null) {

				root = ConfigurationManager.AppSettings["UserDataRoot"];
				root = Path.Combine(root, "assets", mat.ID.ToString());

				storage = this.GetStorage(Server.MapPath(root));
				storage.EnsureRootExist();
			}

			string ext = "";

			if(mat.PreviewImageFile != null) {
				ext = mat.PreviewImageFile.InputStream.GetFileExtension();
				ext = ext == null ? null : ext.ToLower();

				if(mat != null) {
					mat.PreviewFilePath = Path.Combine(root, "pre_" + mat.Updated.Ticks.ToString() + ext).Replace('\\', '/');
					storage.Save(mat.PreviewImageFile.InputStream, "pre_" + mat.Updated.Ticks.ToString() + ext);
				}
			}

			if(mat.AssetFile != null) {
				ext = mat.AssetFile.InputStream.GetFileExtension();
				ext = ext == null ? null : ext.ToLower();

				if(ext != null) {
					mat.AssetFilePath = Path.Combine(root, mat.Updated.Ticks.ToString() + ext).Replace('\\', '/');
					storage.Save(mat.AssetFile.InputStream, mat.Updated.Ticks.ToString() + ext);
				}
			}
		}
	}
}