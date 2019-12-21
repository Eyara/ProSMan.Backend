using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class ToggleTodayTaskHandler : IRequestHandler<ToggleTodayTaskCommand, bool>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }

		public ToggleTodayTaskHandler(
			INonSprintTaskService nonSprintTaskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
		}

		public async Task<bool> Handle(ToggleTodayTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _nonSprintTaskService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			entity.Date = !entity.Date.HasValue || entity.Date.HasValue &&
				entity.Date.Value.Date != DateTime.Today.Date ? DateTime.Today : (DateTime?)null;

			return _nonSprintTaskService.Update(entity);
		}
	}
}
