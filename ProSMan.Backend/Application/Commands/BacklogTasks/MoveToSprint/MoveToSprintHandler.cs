using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
	public class MoveToSprintHandler : IRequestHandler<MoveToSprintCommand, bool>
	{
		private IBacklogTaskService _backlogTaskService { get; set; }
		private ITaskService _taskService { get; set; }

		public MoveToSprintHandler(
			IBacklogTaskService backlogTaskService,
			ITaskService taskService)
		{
			_backlogTaskService = backlogTaskService;
			_taskService = taskService;
		}

		public async Task<bool> Handle(MoveToSprintCommand request, CancellationToken cancellationToken)
		{
			var entity = _backlogTaskService.GetItemById(request.Task.Id);

			if (entity == null)
			{
				return false;
			}
			
			var task = new TaskViewModel
			{
				SprintId = request.Task.SprintId,
				CategoryId = request.Task.CategoryId,
				ProjectId = entity.ProjectId,
				Name = entity.Name,
				Description = entity.Description,
				TimeEstimate = request.Task.TimeEstimate,
				Priority = request.Task.Priority,
				IsFinished = false,
				Date = null,
			};

			_taskService.Add(task);
			return _backlogTaskService.Delete(entity);
		}
	}
}
