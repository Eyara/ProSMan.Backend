using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Projects
{
	public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
	{
		private IProjectService _projectService { get; set; }

		public DeleteProjectHandler(
			IProjectService projectService)
		{
			_projectService = projectService;
		}

		public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
		{
			var entity = _projectService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			return _projectService.Delete(entity);
		}
	}
}
