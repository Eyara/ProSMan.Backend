using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
    public class AddBacklogHandler : IRequestHandler<AddBacklogCommand, bool>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }

		public AddBacklogHandler(
			INonSprintTaskService nonSprintTaskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
		}

		public async Task<bool> Handle(AddBacklogCommand request, CancellationToken cancellationToken)
		{
			request.Task.IsBacklog = true;
			return _nonSprintTaskService.Add(request.Task);
		}
	}
}
