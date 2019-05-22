using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Domain.Base
{
    public class ListRequest<T>
    {
		public List<T> Data { get; set; }

		public ListRequest(List<T> data)
		{
			Data = data;
		}
    }
}
