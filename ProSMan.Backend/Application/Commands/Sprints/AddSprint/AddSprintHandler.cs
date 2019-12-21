using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Commands.Sprints
{
	public class AddSprintHandler : IRequestHandler<AddSprintCommand, bool>
	{
		private ISprintService _sprintService { get; set; }

		public AddSprintHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<bool> Handle(AddSprintCommand request, CancellationToken cancellationToken)
		{
			return _sprintService.Add(request.Sprint);
		}
	}
}
