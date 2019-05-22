using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class UpdateNonSprintTaskCommand : IRequest<bool>
	{
		public NonSprintTaskViewModel Task { get; set; }

		public UpdateNonSprintTaskCommand(NonSprintTaskViewModel task)
		{
			Task = task;
		}
	}
}
