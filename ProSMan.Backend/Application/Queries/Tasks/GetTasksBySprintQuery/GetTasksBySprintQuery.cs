using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Queries.Tasks
{
    public class GetTasksBySprintQuery: IRequest<ListRequest<TaskViewModel>>
    {
		public Guid SprintId { get; set; }

		public GetTasksBySprintQuery(Guid sprintId)
		{
			SprintId = sprintId;
		}
    }
}
