using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
	public class UpdateBacklogTaskCommand : IRequest<bool>
	{
		public BacklogTaskViewModel Task { get; set; }

		public UpdateBacklogTaskCommand(BacklogTaskViewModel task)
		{
			Task = task;
		}
	}
}
