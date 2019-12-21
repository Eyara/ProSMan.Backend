using MediatR;
using ProSMan.Backend.Domain.ViewModels;

namespace ProSMan.Backend.API.Application.Commands.Tasks
{
	public class UpdateTaskCommand : IRequest<bool>
	{
		public TaskViewModel Task { get; set; }

		public UpdateTaskCommand(TaskViewModel task)
		{
			Task = task;
		}
	}
}
