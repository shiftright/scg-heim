using System.Web.Optimization;
using dotless.Core;

namespace ShiftRight.Heim.Bundles {

	public class LessBundle : StyleBundle{
		public LessBundle(string virtualPath): base(virtualPath) {
			Transforms.Insert(0, new LessTransform());
		}

		public LessBundle(string virtualPath, string cdnPath): base(virtualPath, cdnPath) {
			Transforms.Insert(0, new LessTransform());
		}
	}

	public class LessTransform : IBundleTransform {
		public void Process(BundleContext context, BundleResponse response) {
			response.Content = Less.Parse(response.Content);
			response.ContentType = "text/css";
		}
	}
}