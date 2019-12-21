using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
	public class AddBacklogCommand : IRequest<bool>
	{
		public BacklogTaskViewModel Task { get; set; }

		public AddBacklogCommand(BacklogTaskViewModel task)
		{
			Task = task;
		}
	}
}
