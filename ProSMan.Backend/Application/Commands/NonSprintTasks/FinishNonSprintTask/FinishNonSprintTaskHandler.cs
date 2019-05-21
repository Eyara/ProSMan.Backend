using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class FinishNonSprintTaskHandler : IRequestHandler<FinishNonSprintTaskCommand, bool>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }

		public FinishNonSprintTaskHandler(
			INonSprintTaskService nonSprintTaskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
		}

		public async Task<bool> Handle(FinishNonSprintTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _nonSprintTaskService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			entity.IsFinished = !entity.IsFinished;

			return _nonSprintTaskService.Update(entity);
		}
	}
}
