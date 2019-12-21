using MediatR;
using System;

namespace ProSMan.Backend.API.Application.Commands.BacklogTasks
{
	public class DeleteBacklogTaskCommand : IRequest<bool>
	{
		public Guid Id { get; set; }

		public DeleteBacklogTaskCommand(Guid id)
		{
			Id = id;
		}
	}
}
