using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
	public class DeleteBacklogTaskHandler : IRequestHandler<DeleteBacklogTaskCommand, bool>
	{
		private IBacklogTaskService _backlogTaskService { get; set; }

		public DeleteBacklogTaskHandler(
			IBacklogTaskService backlogTaskService)
		{
			_backlogTaskService = backlogTaskService;
		}

		public async Task<bool> Handle(DeleteBacklogTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _backlogTaskService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			return _backlogTaskService.Delete(entity);
		}
	}
}
