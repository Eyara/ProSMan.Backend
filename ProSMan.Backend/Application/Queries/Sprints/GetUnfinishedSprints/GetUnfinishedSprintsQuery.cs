using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Queries.Sprints
{
	public class GetUnfinishedQuery : IRequest<ListRequest<SprintViewModel>>
	{
		public Guid Id { get; set; }

		public GetUnfinishedQuery(Guid id)
		{
			Id = id;
		}
	}
}
