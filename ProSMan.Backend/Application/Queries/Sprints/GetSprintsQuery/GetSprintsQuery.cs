using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Queries.Sprints
{
	public class GetSprintsByIdQuery : IRequest<ListRequest<SprintViewModel>>
	{
		public Guid Id { get; set; }

		public GetSprintsByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}
