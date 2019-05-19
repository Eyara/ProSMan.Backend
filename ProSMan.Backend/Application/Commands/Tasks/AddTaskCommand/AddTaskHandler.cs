using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class AddTaskHandler : IRequestHandler<AddTaskCommand, bool>
	{
		private ITaskService _taskService { get; set; }

		public AddTaskHandler(
			ITaskService taskService)
		{
			_taskService = taskService;
		}

		public async Task<bool> Handle(AddTaskCommand request, CancellationToken cancellationToken)
		{
			return _taskService.Add(request.Task);
		}
	}
}
