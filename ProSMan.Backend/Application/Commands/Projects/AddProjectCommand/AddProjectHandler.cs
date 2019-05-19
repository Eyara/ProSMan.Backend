using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Projects
{
	public class AddProjectHandler : IRequestHandler<AddProjectCommand, bool>
	{
		private IProjectService _projectService { get; set; }

		public AddProjectHandler(
			IProjectService projectService)
		{
			_projectService = projectService;
		}

		public async Task<bool> Handle(AddProjectCommand request, CancellationToken cancellationToken)
		{
			return _projectService.Add(request.Project, request.User);
		}
	}
}
