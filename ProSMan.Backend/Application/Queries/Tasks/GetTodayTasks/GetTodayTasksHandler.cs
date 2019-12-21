using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using ProSMan.Backend.Core.Interfaces.Services;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ProSMan.Backend.API.Application.Queries.Tasks
{
	public class GetTodayTasksHandler : IRequestHandler<GetTodayTasksQuery, ListRequest<TaskViewModel>>
	{
		private readonly ITaskService _taskService;
		private readonly INonSprintTaskService _nonSprintTaskService;
		private readonly IMapper _mapper;

		public GetTodayTasksHandler(
			ITaskService taskService,
			INonSprintTaskService nonSprintTaskService,
			IMapper mapper)
		{
			_taskService = taskService;
			_nonSprintTaskService = nonSprintTaskService;
			_mapper = mapper;
		}

		public async Task<ListRequest<TaskViewModel>> Handle(GetTodayTasksQuery request, CancellationToken cancellationToken)
		{
			var todayTasks = _taskService
				.GetListToday(request.Username)
				.ConvertAll(x => x as TaskViewModel);

			var todayNonSprintTasks = _nonSprintTaskService
				.GetListToday(request.Username)
				.ConvertAll(x => x as NonSprintTaskViewModel)
				.AsQueryable()
				.ProjectTo<TaskViewModel>(_mapper.ConfigurationProvider)
				.ToList();

			todayTasks.AddRange(todayNonSprintTasks);

			return new ListRequest<TaskViewModel>(todayTasks);
		}
	}
}