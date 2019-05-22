using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.NonSprintTasks
{
    public class GetNonSprintsTasksByProjectHandler : IRequestHandler<GetNonSprintsTasksByProjectQuery,
		ListRequest<NonSprintTaskViewModel>>
	{
		private INonSprintTaskService _nonSprintTaskService { get; set; }

		public GetNonSprintsTasksByProjectHandler(
			INonSprintTaskService nonSprintTaskService)
		{
			_nonSprintTaskService = nonSprintTaskService;
		}

		public async Task<ListRequest<NonSprintTaskViewModel>> Handle(GetNonSprintsTasksByProjectQuery request,
			CancellationToken cancellationToken)
		{
			var result = _nonSprintTaskService
				.GetListByProject(request.Id);
			return new ListRequest<NonSprintTaskViewModel>(result
				.ConvertAll(x => x as NonSprintTaskViewModel));
		}
	}
}
