using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;

namespace ShiftRight.Web {

	public class WebFileStorage : ShiftRight.Common.IStorage {

		public string RootPath { get; set; }

		public WebFileStorage() {

			LoadConfiguration();
		}

		public WebFileStorage(string rootPath) {
			RootPath = rootPath;

			LoadConfiguration();
		}

		protected void LoadConfiguration() {

		}

		public void Save(System.IO.Stream stream, string path) {

			path = GetAbsolutePath(path);

			using(StreamWriter writer = new StreamWriter(path)) {
				stream.CopyTo(writer.BaseStream);
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// true if the container did not already exist and was created; otherwise false.
		/// </returns>
		public bool EnsureRootExist() {
			if(!Directory.Exists(RootPath)) {
				try {

					Directory.CreateDirectory(RootPath);
					return true;

				} catch(Exception ex) {
					throw ex;
				}
			}

			return false;
		}

		public System.IO.Stream GetFileStream(string path, FileMode mode) {
			path = GetAbsolutePath(path);

			if(mode == FileMode.Create) {
				string dir = Path.GetDirectoryName(path);
				if(!Directory.Exists(dir)) {
					Directory.CreateDirectory(dir);
				}
			}

			return File.Open(path, mode);
		}

		public string GetAbsolutePath(string relativePath) {
			return Path.Combine(RootPath, relativePath);
		}

		public void DeleteFile(string filename) {
			string path = GetAbsolutePath(filename);

			File.Delete(path);
		}

		public IEnumerable<string> GetFileNames(string directory) {
			string path = GetAbsolutePath(directory);
			return Directory.EnumerateFiles(path);
		}

		public void DeleteDirectory(string path) {
			if(String.IsNullOrEmpty(Path.GetPathRoot(path))) {
				path = GetAbsolutePath(path);
			}

			Directory.Delete(path, true);
		}
	}
}