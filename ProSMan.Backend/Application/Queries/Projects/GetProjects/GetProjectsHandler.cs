using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Projects
{
	public class GetProjectsHandler : IRequestHandler<GetProjectsQuery, ListRequest<ProjectViewModel>>
	{
		private IProjectService _projectService { get; set; }

		public GetProjectsHandler(
			IProjectService projectService)
		{
			_projectService = projectService;
		}

		public async Task<ListRequest<ProjectViewModel>> Handle(GetProjectsQuery request,
			CancellationToken cancellationToken)
		{
			var result = _projectService
				.GetListByUsername(request.Username);
			return new ListRequest<ProjectViewModel>(result
				.ConvertAll(x => x as ProjectViewModel));
		}
	}
}