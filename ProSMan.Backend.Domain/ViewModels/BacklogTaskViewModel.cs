using ProSMan.Backend.Core.Interfaces.Entities;
using System;

namespace ProSMan.Backend.Domain.ViewModels
{
    public class BacklogTaskViewModel: IBacklogTask
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }

		public Guid ProjectId { get; set; }
	}
}
