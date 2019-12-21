using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
	public class FinishSprintHandler : IRequestHandler<FinishSprintCommand, bool>
	{
		private ISprintService _sprintService { get; set; }

		public FinishSprintHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<bool> Handle(FinishSprintCommand request, CancellationToken cancellationToken)
		{
			var entity = _sprintService.GetItemById(request.Id);

			if (entity == null)
			{
				return false;
			}

			return _sprintService.Finish(entity);
		}
	}
}
