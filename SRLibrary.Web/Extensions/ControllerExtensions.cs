using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using ShiftRight.Common;

namespace ShiftRight.Web.Extensions {
	public static class ControllerExtensions {

		public static void SaveUploadedFiles(this System.Web.Mvc.Controller controller) {

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="parameters">Parameters to pass to storage's constructor</param>
		/// <returns></returns>
		public static IStorage GetStorage(this Controller controller, params object[] parameters) {

			string storageType = "ShiftRight.Web.WebFileStorage";

			if(ConfigurationManager.AppSettings["storage:Provider"] != null) {
				storageType = ConfigurationManager.AppSettings["storage:Provider"];
			}

			Type type = Type.GetType(storageType);
			IStorage storage = type.GetConstructor(
				parameters == null? Type.EmptyTypes: parameters.Select(a => a.GetType()).ToArray()
				).Invoke(parameters) as IStorage;
			return storage;
		}
	}
}
