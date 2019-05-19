using MediatR;
using ProSMan.Backend.Domain.Base;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Queries.Tasks
{
	public class GetTodayTasksQuery : IRequest<ListRequest<TaskViewModel>>
	{
		public string Username { get; set; }

		public GetTodayTasksQuery(string username)
		{
			Username = username;
		}
	}
}
