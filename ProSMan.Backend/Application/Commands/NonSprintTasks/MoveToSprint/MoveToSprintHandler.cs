using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class MoveToSprintHandler : IRequestHandler<MoveToSprintCommand, bool>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }
		private ITaskService _taskService { get; set; }

		public MoveToSprintHandler(
			INonSprintTaskService nonSprintTaskService,
			ITaskService taskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
			_taskService = taskService;
		}

		public async Task<bool> Handle(MoveToSprintCommand request, CancellationToken cancellationToken)
		{
			var entity = _nonSprintTaskService.GetItemById(request.Task.Id);

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
				TimeEstimate = entity.TimeEstimate,
				Priority = entity.Priority,
				IsFinished = entity.IsFinished,
				Date = entity.Date,
			};

			_taskService.Add(task);
			return _nonSprintTaskService.Delete(entity);

		}
	}
}
