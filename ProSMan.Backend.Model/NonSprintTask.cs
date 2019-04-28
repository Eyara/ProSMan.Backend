using ProSMan.Backend.Core;
using ProSMan.Backend.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProSMan.Backend.Model
{
    public class NonSprintTask : IEntityBase<Guid>
	{
		public NonSprintTask()
		{
			Id = Guid.NewGuid();
		}
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TimeEstimate { get; set; }
		public Priority Priority { get; set; }
		public bool IsBacklog { get; set; }
		public DateTime? FinishedOn { get; set; }
		public bool IsFinished { get; set; }
		public DateTime? Date { get; set; }

		public Guid ProjectId { get; set; }

		public virtual Project Project { get; set; }
	}
}
