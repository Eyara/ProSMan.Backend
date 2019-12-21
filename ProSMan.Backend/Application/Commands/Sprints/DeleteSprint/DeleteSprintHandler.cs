using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
	public class DeleteSprintHandler : IRequestHandler<DeleteSprintCommand, bool>
	{
		private ISprintService _sprintService { get; set; }

		public DeleteSprintHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<bool> Handle(DeleteSprintCommand request, CancellationToken cancellationToken)
		{
			var entity = _sprintService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			return _sprintService.Delete(entity);
		}
	}
}
