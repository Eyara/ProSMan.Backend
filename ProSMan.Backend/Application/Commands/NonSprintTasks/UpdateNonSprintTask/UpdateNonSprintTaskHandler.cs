using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class UpdateNonSprintTaskHandler : IRequestHandler<UpdateNonSprintTaskCommand, bool>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }

		public UpdateNonSprintTaskHandler(
			INonSprintTaskService nonSprintTaskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
		}

		public async Task<bool> Handle(UpdateNonSprintTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _nonSprintTaskService.GetItemById(request.Task.Id);

			if (entity == null)
			{
				return false;
			}

			return _nonSprintTaskService.Update(request.Task);
		}
	}
}
