using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Tasks
{
	public class GetTodayTasksHandler : IRequestHandler<GetTodayTasksQuery, ListRequest<TaskViewModel>>
	{
		private ITaskService _taskService { get; set; }

		public GetTodayTasksHandler(
			ITaskService taskService)
		{
			_taskService = taskService;
		}

		public async Task<ListRequest<TaskViewModel>> Handle(GetTodayTasksQuery request, CancellationToken cancellationToken)
		{
			var result = _taskService
				.GetListToday(request.Username);
			return new ListRequest<TaskViewModel>(result
				.ConvertAll(x => x as TaskViewModel));
		}
	}
}