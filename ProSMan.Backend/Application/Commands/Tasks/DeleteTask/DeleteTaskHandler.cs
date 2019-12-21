using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, bool>
	{
		private ITaskService _taskService { get; set; }

		public DeleteTaskHandler(
			ITaskService taskService)
		{
			_taskService = taskService;
		}

		public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _taskService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			return _taskService.Delete(entity);
		}
	}
}
