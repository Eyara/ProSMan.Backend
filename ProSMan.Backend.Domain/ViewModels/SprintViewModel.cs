using ProSMan.Backend.Core.Interfaces.Entities;
using System;

namespace ProSMan.Backend.Domain.ViewModels
{
	public class SprintViewModel : ISprint
    {
		public Guid Id { get; set; }
		public string Name { get; set; }
		public DateTime FromDate { get; set; }
		public DateTime? FinishedOn { get; set; }
		public bool IsFinished { get; set; }
		public bool IsDeleted { get; set; }

		public Guid ProjectId { get; set; }
	}
}
