using ProSMan.Backend.Core.Base;
using ProSMan.Backend.Core.Interfaces.Entities;
using System;

namespace ProSMan.Backend.Domain.ViewModels
{
	public class SprintListPagination: PaginationRequest, ISprintListPagination
	{
		public Guid ProjectId { get; set; }
	}
}
