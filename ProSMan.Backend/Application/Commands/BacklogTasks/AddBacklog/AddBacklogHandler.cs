using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
    public class AddBacklogHandler : IRequestHandler<AddBacklogCommand, bool>
	{
		private IBacklogTaskService _backlogTaskService { get; set; }

		public AddBacklogHandler(
			IBacklogTaskService backlogTaskService)
		{
			_backlogTaskService = backlogTaskService;
		}

		public async Task<bool> Handle(AddBacklogCommand request, CancellationToken cancellationToken)
		{
			return _backlogTaskService.Add(request.Task);
		}
	}
}
