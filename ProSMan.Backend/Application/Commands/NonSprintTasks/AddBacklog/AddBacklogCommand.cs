using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class AddBacklogCommand : IRequest<bool>
	{
		public NonSprintTaskViewModel Task { get; set; }

		public AddBacklogCommand(NonSprintTaskViewModel task)
		{
			Task = task;
		}
	}
}
