using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.BacklogTasks
{
    public class GetBacklogByProjectHandler : IRequestHandler<GetBacklogByProjectQuery,
		ListRequest<BacklogTaskViewModel>>
	{
		private IBacklogTaskService _backlogTaskService { get; set; }

		public GetBacklogByProjectHandler(
			IBacklogTaskService backlogTaskService)
		{
			_backlogTaskService = backlogTaskService;
		}

		public async Task<ListRequest<BacklogTaskViewModel>> Handle(GetBacklogByProjectQuery request,
			CancellationToken cancellationToken)
		{
			var result = _backlogTaskService
				.GetListByProject(request.Id);
			return new ListRequest<BacklogTaskViewModel>(result
				.ConvertAll(x => x as BacklogTaskViewModel));
		}
	}
}
