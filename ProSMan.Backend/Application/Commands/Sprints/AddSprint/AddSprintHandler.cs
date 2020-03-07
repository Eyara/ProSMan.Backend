using MediatR;
using ProSMan.Backend.Core.Constants;
using ProSMan.Backend.Core.Exceptions;
using ProSMan.Backend.Core.Interfaces.Services;
using System.Linq;
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
			var isUnfinishedSprintExists = _sprintService
				.GetUnfinishedListByProjectId(request.Sprint.ProjectId)
				.Any();

			if (isUnfinishedSprintExists)
			{
				throw new ActiveSprintAlreadyExistsException(ExceptionConstants.ActiveSprintAlreadyExists);
			}
			return _sprintService.Add(request.Sprint);
		}
	}
}
