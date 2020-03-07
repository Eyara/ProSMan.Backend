using System.Collections.Generic;

namespace ProSMan.Backend.Core.Base
{
	public class PaginationResponse<T>: PaginationRequest
	{
		public int TotalCount { get; set; }
		public int LastPage { get; set; }
		public List<T> Items { get; set; }
	}
}
