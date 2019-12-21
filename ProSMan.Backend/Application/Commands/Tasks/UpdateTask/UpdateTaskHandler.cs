using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, bool>
	{
		private ITaskService _taskService { get; set; }

		public UpdateTaskHandler(
			ITaskService taskService)
		{
			_taskService = taskService;
		}

		public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _taskService.GetItemById(request.Task.Id);

			if (entity == null)
			{
				return false;
			}

			return _taskService.Update(request.Task);
		}
	}
}
