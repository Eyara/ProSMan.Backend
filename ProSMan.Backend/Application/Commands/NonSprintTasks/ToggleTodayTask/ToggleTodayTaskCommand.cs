using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class ToggleTodayTaskCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public ToggleTodayTaskCommand(Guid id)
		{
			Id = id;
		}
	}
}
