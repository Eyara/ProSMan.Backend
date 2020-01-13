using MediatR;
using ProSMan.Backend.Core.Base;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Queries.Sprints
{
	public class GetSprintsByProjectQuery : IRequest<PaginationResponse<SprintViewModel>>
	{
		public SprintListPagination Model { get; set; }

		public GetSprintsByProjectQuery(SprintListPagination model)
		{
			Model = model;
		}
	}
}
