using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
	public class UpdateBacklogTaskHandler : IRequestHandler<UpdateBacklogTaskCommand, bool>
	{
		private IBacklogTaskService _backlogTaskService { get; set; }

		public UpdateBacklogTaskHandler(
			IBacklogTaskService backlogTaskService)
		{
			_backlogTaskService = backlogTaskService;
		}

		public async Task<bool> Handle(UpdateBacklogTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _backlogTaskService.GetItemById(request.Task.Id);

			if (entity == null)
			{
				return false;
			}

			return _backlogTaskService.Update(request.Task);
		}
	}
}
