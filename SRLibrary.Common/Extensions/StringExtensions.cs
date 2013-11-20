using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System {
	public static class StringExtensions {

		/// <summary>
		/// Returns specified string when the target string is empty
		/// </summary>
		/// <param name="str">Target string</param>
		/// <param name="replaceWith">String to be display when the target is empty</param>
		/// <returns>String</returns>
		public static string Default(this string str, string replaceWith = "") {
			if(String.IsNullOrEmpty(str)) {
				return replaceWith;
			}

			return str;
		}

		public static string Capitalize(this string str) {
			if(!String.IsNullOrEmpty(str)) {
				return str.First().ToString().ToUpper() + str.Substring(1);
			}

			return null;
		}

		///// <summary>
		///// Convert 'ThisIsABook' to 'This is a book'
		///// </summary>
		///// <returns></returns>
		//public static string Depascal() {

		//}

		///// <summary>
		///// Replace invalid URL string with URL-safe name
		///// </summary>
		///// <param name="pathString"></param>
		///// <returns></returns>
		//public static string EncodeUrlFriendly(this string pathString) {

		//	if(!String.IsNullOrEmpty(pathString)) {

		//		pathString = pathString.Replace(" & ", "--and--");
		//		pathString = pathString.Replace("&", "-and-");
		//		pathString = pathString.Replace(" ", "_");
		//		return pathString;
		//	}

		//	return null;
		//}

		///// <summary>
		///// 
		///// </summary>
		///// <param name="pathString"></param>
		///// <returns></returns>
		//public static string DecodeUrlFriendly(this string pathString) {

		//	if(!String.IsNullOrEmpty(pathString)) {
		//		pathString = pathString.Replace("_", " ");
		//		pathString = pathString.Replace("--and--", " & ");
		//		pathString = pathString.Replace("-and-", "&");

		//		return pathString;
		//	}

		//	return null;
		//}
	}
}