using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.NonSprintTasks
{
	public class DeleteNonSprintTaskCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public DeleteNonSprintTaskCommand(Guid id)
		{
			Id = id;
		}
	}
}
