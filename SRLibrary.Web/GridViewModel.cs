using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftRight.Web {
	public class GridViewModel {
		public string Title { get; set; }
		public int Level { get; set; }
		public IEnumerable<string> Columns { get; set; }
		public IEnumerable<GridViewRow> Rows { get; set; }

		public GridViewModel() {
			Level = 1;
		}
	}

	public class GridViewRow {
		public IEnumerable<string> Items { get; set; }
		public GridViewModel SubTable { get; set; }
	}
}
