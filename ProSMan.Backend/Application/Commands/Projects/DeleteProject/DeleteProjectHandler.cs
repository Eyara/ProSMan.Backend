using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Projects
{
	public class DeleteProjectHandler : IRequestHandler<DeleteProjectCommand, bool>
	{
		private IProjectService _projectService { get; set; }
		private ICategoryService _categoryService { get; set; }
		private ITaskService _taskService { get; set; }

		public DeleteProjectHandler(
			IProjectService projectService,
			ICategoryService categoryService,
			ITaskService taskService)
		{
			_projectService = projectService;
			_categoryService = categoryService;
			_taskService = taskService;
		}

		public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
		{
			var entity = _projectService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			_taskService.DeleteByProjectId(entity.Id);
			_categoryService.DeleteByProjectId(entity.Id);

			return _projectService.Delete(entity);
		}
	}
}
