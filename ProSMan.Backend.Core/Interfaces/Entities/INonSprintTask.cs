using ProSMan.Backend.Core.Enums;
using System;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
    public interface INonSprintTask
    {
		Guid Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		int TimeEstimate { get; set; }
		Priority Priority { get; set; }
		bool IsFinished { get; set; }
		DateTime? Date { get; set; }

		Guid ProjectId { get; set; }
	}
}
