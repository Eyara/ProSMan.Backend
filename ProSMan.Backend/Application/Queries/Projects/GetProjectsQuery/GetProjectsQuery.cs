using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Queries.Projects
{
	public class GetProjectsQuery : IRequest<ListRequest<ProjectViewModel>>
	{
		public string Username { get; set; }

		public GetProjectsQuery(string username)
		{
			Username = username;
		}
	}
}
