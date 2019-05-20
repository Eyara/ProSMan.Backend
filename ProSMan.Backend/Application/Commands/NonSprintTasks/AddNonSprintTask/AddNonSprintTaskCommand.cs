using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class AddNonSprintTaskCommand : IRequest<bool>
	{
		public NonSprintTaskViewModel Task { get; set; }

		public AddNonSprintTaskCommand(NonSprintTaskViewModel task)
		{
			Task = task;
		}
	}
}
