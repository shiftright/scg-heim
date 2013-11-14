using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ShiftRight.Common {

	public interface IStorage {

		void Save(Stream stream, string path);

		Stream GetFileStream(string path, FileMode mode);

		/// <summary>
		/// Returns absolute path to the given relativePath
		/// </summary>
		/// <param name="relativePath"></param>
		string GetAbsolutePath(string relativePath);

		void DeleteFile(string filename);

		/// <summary>
		/// Returns list of files in the given directory
		/// </summary>
		IEnumerable<string> GetFileNames(string directory);

		/// <summary>
		/// 
		/// </summary>
		/// <returns>
		/// true if the container did not already exist and was created; otherwise false.
		/// </returns>
		bool EnsureRootExist();

		/// <summary>
		/// Deletes the specified directory and any subdirectories and files in the directory.
		/// </summary>
		void DeleteDirectory(string path);
	}
}
