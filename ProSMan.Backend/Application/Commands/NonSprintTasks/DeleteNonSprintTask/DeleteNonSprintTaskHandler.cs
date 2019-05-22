using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class DeleteNonSprintTaskHandler : IRequestHandler<DeleteNonSprintTaskCommand, bool>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }

		public DeleteNonSprintTaskHandler(
			INonSprintTaskService nonSprintTaskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
		}

		public async Task<bool> Handle(DeleteNonSprintTaskCommand request, CancellationToken cancellationToken)
		{
			var entity = _nonSprintTaskService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			return _nonSprintTaskService.Delete(entity);
		}
	}
}
