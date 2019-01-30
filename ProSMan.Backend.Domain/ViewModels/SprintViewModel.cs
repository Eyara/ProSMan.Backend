using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class SprintViewModel
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime ToDate { get; set; }
		public bool IsFinished { get; set; }

		public Guid ProjectId { get; set; }
	}
}
