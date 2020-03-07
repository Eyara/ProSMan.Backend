using System;

namespace ProSMan.Backend.Core.Interfaces.Entities
{
	public interface ISprintListPagination: IPagination
	{
		Guid ProjectId { get; set; }
	}
}
