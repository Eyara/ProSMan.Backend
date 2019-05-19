using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System;

namespace ProSMan.Backend.API.Application.Queries.Projects
{
	public class GetProjectsByIdQuery : IRequest<ListRequest<ProjectViewModel>>
	{
		public Guid Id { get; set; }

		public GetProjectsByIdQuery(Guid id)
		{
			Id = id;
		}
	}
}
