using System;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
	public interface IBacklogTask
	{
		Guid Id { get; set; }
		string Name { get; set; }
		string Description { get; set; }
		Guid ProjectId { get; set; }
	}
}
