using ProSMan.Backend.Core.Enums;
using ProSMan.Backend.Core.Interfaces.Entities;
using System;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class NonSprintTaskViewModel: INonSprintTask
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int TimeEstimate { get; set; }
		public Priority Priority { get; set; }
		public bool IsBacklog { get; set; }
		public bool IsFinished { get; set; }
		public DateTime? FinishedOn { get; set; }
		public DateTime? Date { get; set; }

		public Guid ProjectId { get; set; }
	}
}
