using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Sprints
{
	public class GetUnfinishedSprintsHandler : IRequestHandler<GetUnfinishedQuery, ListRequest<SprintViewModel>>
	{
		private ISprintService _sprintService { get; set; }

		public GetUnfinishedSprintsHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<ListRequest<SprintViewModel>> Handle(GetUnfinishedQuery request,
			CancellationToken cancellationToken)
		{
			var result = _sprintService
				.GetUnfinishedListByProjectId(request.Id);
			return new ListRequest<SprintViewModel>(result
				.ConvertAll(x => x as SprintViewModel));
		}
	}
}