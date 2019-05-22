using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
	public class UpdateSprintHandler : IRequestHandler<UpdateSprintCommand, bool>
	{
		private ISprintService _sprintService { get; set; }

		public UpdateSprintHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<bool> Handle(UpdateSprintCommand request, CancellationToken cancellationToken)
		{
			var entity = _sprintService.GetItemById(request.Sprint.Id);

			if (entity == null)
			{
				return false;
			}

			return _sprintService.Update(request.Sprint);
		}
	}
}
