using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Sprints
{
	public class GetSprintsByProjectHandler : IRequestHandler<GetSprintsByProjectQuery, ListRequest<SprintViewModel>>
	{
		private ISprintService _sprintService { get; set; }

		public GetSprintsByProjectHandler(
			ISprintService sprintService)
		{
			_sprintService = sprintService;
		}

		public async Task<ListRequest<SprintViewModel>> Handle(GetSprintsByProjectQuery request,
			CancellationToken cancellationToken)
		{
			var result = _sprintService
				.GetListByProjectId(request.Id);
			return new ListRequest<SprintViewModel>(result
				.ConvertAll(x => x as SprintViewModel));
		}
	}
}