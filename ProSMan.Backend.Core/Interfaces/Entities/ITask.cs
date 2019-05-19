using ProSMan.Backend.Core.Enums;
using System;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
    public interface ITask
    {
		Guid Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		DateTime? Date { get; set; }
		int TimeEstimate { get; set; }
		Priority Priority { get; set; }
		bool IsFinished { get; set; }
		DateTime? FinishedOn { get; set; }
	}
}
