using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Queries.BacklogTasks
{
    public class GetBacklogByProjectQuery : IRequest<ListRequest<BacklogTaskViewModel>>
	{
		public Guid Id { get; set; }

		public GetBacklogByProjectQuery(Guid id)
		{
			Id = id;
		}
	}
}
