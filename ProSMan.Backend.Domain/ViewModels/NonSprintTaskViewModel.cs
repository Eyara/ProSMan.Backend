using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class NonSprintTaskViewModel
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TimeEstimate { get; set; }
		public Priority Priority { get; set; }
		public bool IsFinished { get; set; }
		public DateTime? Date { get; set; }

		public Guid ProjectId { get; set; }
	}
}
