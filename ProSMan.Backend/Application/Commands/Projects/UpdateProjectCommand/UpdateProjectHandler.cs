using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Projects
{
	public class UpdateProjectHandler : IRequestHandler<UpdateProjectCommand, bool>
	{
		private IProjectService _projectService { get; set; }

		public UpdateProjectHandler(
			IProjectService projectService)
		{
			_projectService = projectService;
		}

		public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
		{
			var entity = _projectService.GetItemById(request.Project.Id);

			if (entity == null)
			{
				return false;
			}

			return _projectService.Update(request.Project);
		}
	}
}
