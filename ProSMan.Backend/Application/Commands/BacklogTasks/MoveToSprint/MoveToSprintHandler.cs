using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
	public class MoveToSprintHandler : IRequestHandler<MoveToSprintCommand, bool>
	{
		private IBacklogTaskService _backlogTaskService { get; set; }
		private ITaskService _taskService { get; set; }
		private ISprintService _sprintService { get; set; }

		public MoveToSprintHandler(
			IBacklogTaskService backlogTaskService,
			ITaskService taskService,
			ISprintService sprintService)
		{
			_backlogTaskService = backlogTaskService;
			_taskService = taskService;
			_sprintService = sprintService;
		}

		public async Task<bool> Handle(MoveToSprintCommand request, CancellationToken cancellationToken)
		{
			var entity = _backlogTaskService.GetItemById(request.Task.Id);

			if (entity == null)
			{
				return false;
			}

			var nonFinishedSprint = _sprintService
				.GetListByProjectId(entity.ProjectId)
				.FirstOrDefault(sprint => !sprint.IsFinished);

			if (nonFinishedSprint == null)
			{
				return false;
			}
			
			var task = new TaskViewModel
			{
				SprintId = nonFinishedSprint.Id,
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
