using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
    public class AddNonSprintTaskHandler : IRequestHandler<AddNonSprintTaskCommand, bool>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }

		public AddNonSprintTaskHandler(
			INonSprintTaskService nonSprintTaskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
		}

		public async Task<bool> Handle(AddNonSprintTaskCommand request, CancellationToken cancellationToken)
		{
			return _nonSprintTaskService.Add(request.Task);
		}
	}
}
