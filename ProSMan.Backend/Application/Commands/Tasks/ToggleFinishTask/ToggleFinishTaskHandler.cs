using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class ToggleFinishTaskHandler : IRequestHandler<ToggleFinishTaskCommand, bool>
	{
		private ITaskService _taskService { get; set; }

		public ToggleFinishTaskHandler(
			ITaskService taskService)
		{
			_taskService = taskService;
		}

		public async Task<bool> Handle(ToggleFinishTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _taskService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			entity.IsFinished = !entity.IsFinished;
			entity.FinishedOn = entity.IsFinished ? DateTime.UtcNow : (DateTime?)null;

			return _taskService.Update(entity);
		}
	}
}
