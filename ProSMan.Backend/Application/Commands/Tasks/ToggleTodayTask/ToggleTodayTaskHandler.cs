using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class ToggleTodayTaskHandler : IRequestHandler<ToggleTodayTaskCommand, bool>
	{
		private ITaskService _taskService { get; set; }

		public ToggleTodayTaskHandler(
			ITaskService taskService)
		{
			_taskService = taskService;
		}

		public async Task<bool> Handle(ToggleTodayTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _taskService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			entity.Date = !entity.Date.HasValue || entity.Date.HasValue &&
				entity.Date.Value.Date != DateTime.Today.Date ? DateTime.Today : (DateTime?)null;

			return _taskService.Update(entity);
		}
	}
}
